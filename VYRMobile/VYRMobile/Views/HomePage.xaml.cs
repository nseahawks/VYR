using Rg.Plugins.Popup.Extensions;
using System;
using VYRMobile.Data;
using VYRMobile.Models;
using VYRMobile.Services;
using VYRMobile.ViewModels;
using VYRMobile.Views.Popups;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace VYRMobile
{
    public partial class HomePage : ContentPage
    {
        public Command LocationCheckingCommand { get; set; }
        private static HomePage _instance;
        public static HomePage Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new HomePage();

                return _instance;
            }
        }
        public HomePage()
        {
            InitializeComponent();
            BindingContext = HomeViewModel.Instance;
            LocationCheckingCommand = new Command(LocationChecking);

            QR.Clicked += QR_Clicked;
            btnCall.Clicked += btnCall_Clicked;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
        private void QR_Clicked(object sender, EventArgs e)
        {
            Escaner();
        }
        private async void Escaner()
        {
            if(App.ApplicationUserRole == "Qr" || App.ApplicationUserRole == "Patrol" || App.ApplicationUserRole == "Supervisor" || App.ApplicationUserRole == "Master")
            {
                var scannerPage = new ZXingScannerPage();

                scannerPage.Title = "Lector de QR";

                scannerPage.OnScanResult += (result) =>
                {
                    scannerPage.IsScanning = false;

                    App.AntennaId = result.ToString();

                    LocationChecking();
                };

                await Navigation.PushModalAsync(scannerPage);
            }
            else if(App.ApplicationUserRole == "User")
            {
                await Navigation.PushPopupAsync(new LocationReportPopup());
            }
            else
            {
                await DisplayAlert("Denegado", "Tu usuario no tiene rondas", "Aceptar");
            }
            /*var scannerPage = new ZXingScannerPage();
            
            scannerPage.Title = "Lector de QR";

            scannerPage.OnScanResult += (result) =>
            {
                scannerPage.IsScanning = false;

                App.AntennaId = result.ToString();

                LocationChecking();
            };

            await Navigation.PushModalAsync(scannerPage);*/
            await Navigation.PushPopupAsync(new LocationReportPopup());
        }

        private async void btnCall_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new CallPopup());
        }
        private void LocationChecking()
        {
            locationChecking.Command.Execute(null);
        }

        private async void sosButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new LoadingPopup("Enviando informe..."));
            Report sosReport = new Report()
            {
                Title = "Emergencia",
                Description = "El trabajador requiere de ayuda urgente",
                Created = DateTime.Now
            };

            var isSuccess = await ReportsStore.Instance.SendEventualityReportAsync(sosReport);

            await Navigation.PopPopupAsync();

            if(isSuccess)
            {
                DependencyService.Get<IToast>().LongToast("Informe enviado");
            }
            else
            {
                await DisplayAlert("Error", "No se pudo conectar, intente de nuevo", "Aceptar");
            }
        }
    }
}