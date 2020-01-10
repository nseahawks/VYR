using MvvmHelpers;
using System;
using System.Threading.Tasks;
using VYRMobile.Models;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace VYRMobile.ViewModels
{
    class QRViewModel : BaseViewModel
    {
        public ZXingScannerPage Scanner { get; set; }
        public QR QR { get; set; }

        public Command SendMessageCommand { get; }
        public Command ConnectCommand { get; }
        public Command DisconnectCommand { get; }
        //public Command ScanCommand { get; }
        bool isConnected;
        public bool IsConnected
        {
            get => isConnected;
            set
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    SetProperty(ref isConnected, value);
                });
            }
        }

        public QRViewModel()
        {

            Scanner = new ZXingScannerPage();
            QR = new QR();

            //ScanCommand = new Command(async () => await Scan());


        }



     

        //private async Task Scan()
        //{
        //    var scannerPage = new ZXingScannerPage();

        //    scannerPage.Title = "Lector de QR";
        //    scannerPage.OnScanResult += (result) =>
        //    {
        //        scannerPage.IsScanning = false;

        //        //Device.BeginInvokeOnMainThread(() =>
        //        //{
        //        //    Navigation.PopAsync();
        //        //    DisplayAlert("Valor Obtenido", result.Text, "OK");
        //        //    VM.TrackMessage.User = "QR";
        //        //    VM.TrackMessage.Message = $"code: {result.Text}";
        //        //    //VM.SendMessageCommand.Execute(null);

        //        //});
        //    };

        //    //await Navigation.PushAsync(scannerPage);
        //}
    }
}
