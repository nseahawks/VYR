using Rg.Plugins.Popup.Extensions;
using System;
using VYRMobile.ViewModels;
using VYRMobile.Views.Popups;
using Xamarin.Forms;

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