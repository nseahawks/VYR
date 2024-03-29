﻿using Android.Content;
using Android.Locations;
using Plugin.CloudFirestore;
using Plugin.CloudFirestore.Extensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VYRMobile.Data;
using VYRMobile.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingPage : ContentPage
    {
        bool isWaiting;
        string _userId;
        public LoadingPage()
        {
            InitializeComponent();

            //StartGeofence();
            isWaiting = true;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Charge();
            /*AwaitForFirebaseAuthorization();
            isWaiting = false;
            Application.Current.MainPage = new NavigationPage(new MenuPage());*/
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
        private async void AwaitForFirebaseAuthorization()
        {
            _userId = await SecureStorage.GetAsync("id");

            string repository;

            if (App.ApplicationUserRole == "Supervisor")
            {
                repository = "supervisorsApp";
            }
            else
            {
                repository = "usersApp";
            }

            var document = CrossCloudFirestore.Current.Instance
            .GetCollection(repository)
            .GetDocument(_userId)
            .AsObservable()
            .Subscribe(document =>
            {
                var test = document.Data["Status"].ToString();

                if (test == "ACCEPTED")
                {
                    loadingMessageLabel.Text = "Inicio de sesión confirmado" + Environment.NewLine + "Cargando componentes...";
                    loadingMessageLabel.TextColor = Color.FromHex("#05A66A");
                    isWaiting = false;
                    Application.Current.MainPage = new NavigationPage(new TabMenuPage());
                    if (App.ApplicationUserRole != "Supervisor")
                    {
                        DependencyService.Get<IStartAlarmService>().StartService();
                    }
                }
                else if (test == "CANCELED")
                {
                    loadingMessageLabel.Text = "Acceso denegado por monitoreo" + Environment.NewLine + "Volviendo al inicio...";
                    loadingMessageLabel.TextColor = Color.FromHex("#DD0808");
                    isWaiting = false;
                    App.IsUserLoggedIn = false;
                    Application.Current.MainPage = new NavigationPage(new LoginPage());
                }
                else
                {
                    isWaiting = true;
                }
            });
        }
        private async void Charge()
        {
            //while(isWaiting == true)
            //{
                await Task.Delay(10);
                Application.Current.MainPage = new NavigationPage(new TabMenuPage());
            //}
        }
        private async void StartGeofence() 
        {
            if(App.ApplicationUserRole == "Vigilant")
            {
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                LocationManager locManager = (LocationManager)Android.App.Application.Context.GetSystemService(Context.LocationService);
                bool isGPSEnabled = locManager.IsProviderEnabled(LocationManager.GpsProvider);
                
                if (status != PermissionStatus.Granted)
                {
                    status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

                    if (status != PermissionStatus.Granted)
                    {
                        await DisplayAlert("Denegado", "Por favor concede los permisos del GPS para continuar", "Aceptar");
                        await Navigation.PopToRootAsync();
                    }
                }
                else if(isGPSEnabled == false)
                {
                    await App.Current.MainPage.DisplayAlert("Fallido", "Por favor activa tu GPS para continuar", "Aceptar");
                    await Navigation.PopToRootAsync();
                }
                else
                {
                    try
                    {
                        var locationPoints = await ReportsStore.Instance.GetAntenasAsync();
                        List<Position> list = new List<Position>();

                        foreach (var locationPoint in locationPoints)
                        {
                            list.Add(new Position(locationPoint.Latitude, locationPoint.Longitude));
                        }

                        IGeofenceService geofencesService = DependencyService.Get<IGeofenceService>();

                        var location = await Geolocation.GetLocationAsync();
                        if (location == null)
                        {
                            location = await Geolocation.GetLastKnownLocationAsync();
                        }

                        geofencesService.SetGeofences(list, new Position(location.Latitude, location.Longitude));
                    }
                    catch
                    {
                        await DisplayAlert("Error", "Hubo un problema al procesar la informacion, intentelo de nuevo", "Aceptar");
                        await Navigation.PopToRootAsync();
                    }
                }
            }
        }
    }
}