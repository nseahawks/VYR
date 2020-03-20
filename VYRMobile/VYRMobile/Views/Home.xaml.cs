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
        public static readonly BindableProperty ShowMapCommandProperty =
           BindableProperty.Create(nameof(ShowMapCommand), typeof(Command), typeof(Home), null, BindingMode.TwoWay);

        PuntoViewModel pvm = new PuntoViewModel();
        public ICommand LoadCommand { get; }

        public Home()
        {
            InitializeComponent();
            BindingContext = new PuntoViewModel();
            LoadCommand = new Command(LoadView);
            _client = new ApiHelper();

            QR.Clicked += QR_Clicked;
            //btnCall.Clicked += btnCall_Clicked;
            ShowMapCommand = new Command(ShowMap);
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        public void LoadView()
        {
            antenasView.ItemsSource = null;
            antenasView.ItemsSource = pvm.Puntos;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }
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

                string antenna = result.ToString();
                pvm.Antenna = antenna;
                pvm.CheckAntenna.Execute(null);

                Device.BeginInvokeOnMainThread(async() =>
                {
                    await Navigation.PopModalAsync();

                    await DisplayAlert("Valor Obtenido", result.Text, "OK");
                });
            };
            
            await Navigation.PushModalAsync(scannerPage);
        }

        /*private async void btnCall_Clicked(object sender, EventArgs e)
        {
            FirestoreAlarm alarmDocument = new FirestoreAlarm()
            {
                LocationName = "Seahawks",
                Location = new GeoPoint(18.4047, -70.0328),
                Type = "Alarm"
            }; 
            await Navigation.PushPopupAsync(new AlarmPopup(alarmDocument.LocationName, alarmDocument.Location, alarmDocument.Type));
        }*/
    }
}