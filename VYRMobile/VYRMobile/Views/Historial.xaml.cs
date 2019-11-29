using Plugin.Geolocator;
using Plugin.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VYRMobile.ViewModels;
using VYRMobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

namespace VYRMobile
{
    public partial class Historial : ContentPage
    {
        
        public Historial()
        {
            InitializeComponent();
            BindingContext = new CallViewModel();
            BindingContext = new CronoViewModel();
            BindingContext = new QRViewModel();

            /*btnStart.Clicked += BtnStart_Clicked;
            btnStop.Clicked += BtnStop_Clicked;*/
            QR.Clicked += QR_Clicked;
            CallFrancisco.Clicked += CallFrancisco_clicked;
            alert.Clicked += alert_clicked;

            /*Menu.ItemTapped += async (sender, e) =>
            {
                var evnt = (SelectedItemChangedEventArgs)e;
                Notifier.Text = (string)evnt.SelectedItem;
                await Task.Delay(2000);
                Notifier.Text = "";

            };*/
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();

            AlertMain();
        }

        private async void AlertMain()
        {
            await Task.Delay(5000);
            DisplayAlert("Alerta", "ALARMA SEAHAWKS", "ACEPTAR");
            showMap();
        }
        private async void alert_clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Alerta", "ALARMA SEAHAWKS", "ACEPTAR");
            showMap();
        }

        private void showMap()
        {
            Navigation.PushModalAsync(new Mapa2());
        }
        /*private void BtnStop_Clicked(object sender, EventArgs e)
        {
            btnStop.IsVisible = false;
            btnStart.IsVisible = true;
        }

        private void BtnStart_Clicked(object sender, EventArgs e)
        {
            btnStart.IsVisible = false;
            btnStop.IsVisible = true;
        }*/
        private void QR_Clicked(object sender, EventArgs e)
        {
            Escaner();
        }
        private async void Escaner()
        {
            var scannerPage = new ZXingScannerPage();

            scannerPage.Title = "Lector de QR";

            scannerPage.OnScanResult += (result) =>
            {
                scannerPage.IsScanning = false;
                Device.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PopModalAsync();

                    DisplayAlert("Valor Obtenido", result.Text, "OK");
                    seaCheckbox.IsChecked = true;
                });


            };

            await Navigation.PushModalAsync(scannerPage);
        }

        private void CallFrancisco_clicked(object sender, EventArgs e)
        {
            var phoneCallTask = CrossMessaging.Current.PhoneDialer;
            if (phoneCallTask.CanMakePhoneCall)
            {
                phoneCallTask.MakePhoneCall("+18097966316", "Francisco Rojas");
            }
        }

        /*async void OnButtonClicked(object sender, EventArgs e)
            {
                Geocoder geoCoder = new Geocoder();
                try
                {
                    var locator = CrossGeolocator.Current;
                    locator.DesiredAccuracy = 50;
                    var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(2));
                    double? latitude = Convert.ToDouble(position.Latitude);
                    double? longitude = Convert.ToDouble(position.Longitude);
                    if (latitude != null && longitude != null)
                    {
                        var revposition = new Position(latitude.Value, longitude.Value);
                        var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(revposition);
                        foreach (var address in possibleAddresses)
                            addressLabel.Text += address + "\n";
                    }
                    else addressLabel.Text += "error";
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Notification", "Unable to get GPS Location " + ex, "Ok");
                }
            }*/
    }
}