using Plugin.LocalNotifications;
using Plugin.Messaging;
using System;
using System.Threading.Tasks;
using VYRMobile.ViewModels;
using VYRMobile.Views;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace VYRMobile
{
    public partial class Home : ContentPage
    {
        public static readonly BindableProperty ShowMapCommandProperty =
           BindableProperty.Create(nameof(ShowMapCommand), typeof(Command), typeof(Home), null, BindingMode.TwoWay);


        public Home()
        {
            InitializeComponent();
            //BindingContext = new CronoViewModel();
            //BindingContext = new TareaViewModel();
            BindingContext = new PuntoViewModel();

            //AlertMain();
            //btnStart.Clicked += BtnStart_Clicked;
            //btnStop.Clicked += BtnStop_Clicked;
            /*BindingContext = new CallViewModel();
            BindingContext = new QRViewModel();*/
            QR.Clicked += QR_Clicked;
            ShowMapCommand = new Command(ShowMap);
            CallFrancisco.Clicked += CallFrancisco_clicked;
            //alert.Clicked += alert_clicked;  
        }
        //private void BtnStop_Clicked(object sender, EventArgs e)
        //{
        //    btnStop.IsVisible = false;
        //    btnStart.IsVisible = true;
        //}

        //private void BtnStart_Clicked(object sender, EventArgs e)
        //{
        //    btnStart.IsVisible = false;
        //    btnStop.IsVisible = true;
        //}

        /*private async void alert_clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Alerta", "ALARMA SEAHAWKS", "ACEPTAR");
            showMap();
        }

        private void showMap()
        {
            Navigation.PushModalAsync(new Mapa2());
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

        }
        ..............

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

        }*/
        /*protected override void OnAppearing()
        {
            base.OnAppearing();

            AlertMain();
        }

        private async void AlertMain()
        {
            await Task.Delay(5000);
            DisplayAlert("Alerta", "ALARMA SEAHAWKS", "ACEPTAR");
            
        }*/
        public Command ShowMapCommand
        {
            get { return (Command)GetValue(ShowMapCommandProperty); }
            set { SetValue(ShowMapCommandProperty, value); }
        }

        private void ShowMap()
        {
            Navigation.PushModalAsync(new Mapa2());
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
                    Navigation.PopModalAsync();

                    DisplayAlert("Valor Obtenido", result.Text, "OK");
                    //seaCheckbox.IsChecked = true;
                });
            };
            
            await Navigation.PushModalAsync(scannerPage);
        }

        private void CallFrancisco_clicked(object sender, EventArgs e)
        {
            CrossLocalNotifications.Current.Show("NUEVA ALARMA", "Seahawks");
            /*var phoneCallTask = CrossMessaging.Current.PhoneDialer;
            if (phoneCallTask.CanMakePhoneCall)
            {
                phoneCallTask.MakePhoneCall("+18097966316", "Francisco Rojas");
            }*/
        }
    }
}