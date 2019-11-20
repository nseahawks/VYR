using Microsoft.AspNetCore.SignalR.Client;
using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.Models;
using Xamarin.Forms.Xaml;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;

namespace VYRMobile.ViewModels
{
    class QRViewModel : BaseViewModel
    {
        HubConnection _hub;
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
            SendMessageCommand = new Command(async () => await SendMessage());
            ConnectCommand = new Command(async () => await Connect());
            DisconnectCommand = new Command(async () => await Disconnect());
            //ScanCommand = new Command(async () => await Scan());

            _hub = new HubConnectionBuilder()
               .WithUrl("https://vyrapi.azurewebsites.net/hubs/trackHub")
               .Build();

            _hub.Closed += async (error) =>
            {
                //SendLocalMessage("Connection Closed...");
                IsConnected = false;
                await Task.Delay(3000);
                await Connect();
            };

            _hub.On<string, string>("ReceiveMessage", (user, message) => {
                var finalMessage = $"{user} says {message}";

                //SendLocalMessage(finalMessage);
            });
        }

        private async Task SendMessage()
        {
            try
            {
                IsBusy = true;
                await _hub.InvokeAsync("ReceiveMessage", "QR: ",
                    Scanner.Result
                    );
            }
            catch (Exception ex)
            {
                await _hub.InvokeAsync("ReceiveMessage", "QR: ",
                    ex.Message
                    );
                //SendLocalMessage($"Send failed: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task Connect()
        {
            if (IsConnected)
                return;
            try
            {
                await _hub.StartAsync();
                IsConnected = true;
                await _hub.InvokeAsync("ReceiveMessage", "QR",
                    "Connected"
                    );
                //SendLocalMessage("Connected...QR");
            }
            catch (Exception ex)
            {
                //SendLocalMessage($"Connection error: {ex.Message}");
            }
        }

        private async Task Disconnect()
        {
            if (!IsConnected)
                return;
            await _hub.StopAsync();
            IsConnected = false;
            //SendLocalMessage("Disconnected...QR");
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
