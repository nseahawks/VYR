using Plugin.CloudFirestore;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Net.Http;
using System.Windows.Input;
using VYRMobile.Helper;
using VYRMobile.Models;
using VYRMobile.ViewModels;
using VYRMobile.Views;
using VYRMobile.Views.Popups;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace VYRMobile
{
    public partial class Home : ContentPage
    {
        HttpClient _client;

        PuntoViewModel pvm = new PuntoViewModel();
        public Command LocationCheckingCommand { get; set; }
        private static Home _instance;
        public static Home Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Home();

                return _instance;
            }
        }
        public Home()
        {
            InitializeComponent();
            BindingContext = PuntoViewModel.Instance;
            LocationCheckingCommand = new Command(LocationChecking);

            _client = new ApiHelper();

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
    }
}