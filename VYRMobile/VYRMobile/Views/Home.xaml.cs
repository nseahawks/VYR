using Plugin.Messaging;
using System;
using VYRMobile.ViewModels;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace VYRMobile
{
    public partial class Home : ContentPage
    {

        public Home()
        {
            InitializeComponent();
            BindingContext = new CronoViewModel();
            BindingContext = new CallViewModel();
            BindingContext = new QRViewModel();
            QR.Clicked += QR_Clicked;

            CallFrancisco.Clicked += CallFrancisco_clicked;
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
            var scannerPage = new ZXingScannerPage();
            
            scannerPage.Title = "Lector de QR";

            scannerPage.OnScanResult += (result) =>
            {
                scannerPage.IsScanning = false;
                Device.BeginInvokeOnMainThread(() =>
                {
                    Navigation.PopAsync();

                    DisplayAlert("Valor Obtenido", result.Text, "OK");
                });

            };
            
            await Navigation.PushAsync(scannerPage);
        }

        private void CallFrancisco_clicked(object sender, EventArgs e)
            {
                var phoneCallTask = CrossMessaging.Current.PhoneDialer;
                if (phoneCallTask.CanMakePhoneCall)
                {
                    phoneCallTask.MakePhoneCall("+18097966316", "Francisco Rojas");
                }
            }
    }
}