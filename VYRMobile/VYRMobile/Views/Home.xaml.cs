using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Messaging;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ZXing.Mobile;
using ZXing;
using ZXing.Net.Mobile.Forms;

namespace VYRMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Home : ContentPage
    {
        

        public Home()
        {
            InitializeComponent();
            BindingContext = new CronoViewModel();
            BindingContext = new CallViewModel();
            QR.Clicked += QR_Clicked;
            Route.Clicked += Route_clicked;
            //CallFrancisco.Clicked += CallFrancisco_clicked;
        }

        private void Route_clicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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

        /*private void CallFrancisco_clicked(object sender, EventArgs e)
            {
                var phoneCallTask = CrossMessaging.Current.PhoneDialer;
                if (phoneCallTask.CanMakePhoneCall)
                {
                    phoneCallTask.MakePhoneCall("+18097966316", "Francisco Rojas");
                }
            }*/
    }
}