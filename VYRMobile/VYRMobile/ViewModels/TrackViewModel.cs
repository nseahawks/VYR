using Android.OS;
using Microsoft.AspNetCore.SignalR.Client;
using MvvmHelpers;
using System;
using System.Threading.Tasks;
using VYRMobile.Models;
using Xamarin.Forms;

namespace VYRMobile.ViewModels
{
    class TrackViewModel : BaseViewModel
    {
        HubConnection _hub;

        public TrackMessage TrackMessage { get; }

        public ObservableRangeCollection<TrackMessage> Messages { get; }

        public Command SendMessageCommand { get; }
        public Command ConnectCommand { get; }
        public Command DisconnectCommand { get; }
        bool isConnected;
        public bool IsConnected
        {
            get => isConnected;
            set {
                Device.BeginInvokeOnMainThread(() =>
                {
                    SetProperty(ref isConnected, value);
                });
            } 
        }

        Random random;
        string fing = Build.Fingerprint;
        string url = "10.0.0.49";
        bool isEmulator = false;
        public TrackViewModel()
        {
            TrackMessage = new TrackMessage();
            Messages = new ObservableRangeCollection<TrackMessage>();
            SendMessageCommand = new Command(async () => await SendMessage());
            ConnectCommand = new Command(async () => await Connect());
            DisconnectCommand = new Command(async () => await Disconnect());
            
            random = new Random();  
            if (fing != null)
            {
                isEmulator = fing.Contains("vbox") || fing.Contains("generic");
            }

            if (isEmulator)
            {
                url = "10.0.2.2";
            }

            _hub = new HubConnectionBuilder()
                //.WithUrl($"http://{url}:5000/hubs/trackHub")
                .WithUrl("https://vyrapi.azurewebsites.net/hubs/trackHub")
                .Build();

            _hub.Closed += async (error) =>
            {
                SendLocalMessage("Connection Closed...");
                IsConnected = false;
                await Task.Delay(random.Next(0, 5) * 1000);
                await Connect();
            };

            _hub.On<string, string>("ReceiveMessage", (user, message) => {
                var finalMessage = $"{user} says {message}"; 

                SendLocalMessage(finalMessage);
            });
        }


        async Task Connect()
        {
            if (IsConnected)
                return;
            try
            {
                await _hub.StartAsync();
                IsConnected = true;
                SendLocalMessage("Connected...");
            }
            catch (Exception ex)
            {
                SendLocalMessage($"Connection error: {ex.Message}");
            }
        }

        async Task Disconnect()
        {
            if (!IsConnected)
                return;
            await _hub.StopAsync();
            IsConnected = false;
            SendLocalMessage("Disconnected...");
        }

        async Task SendMessage()
        {
            try
            {
                IsBusy = true;
                await _hub.InvokeAsync("SendMessage", 
                    TrackMessage.User,
                    TrackMessage.Message
                    );
            }
            catch (Exception ex)
            {
                SendLocalMessage($"Send failed: {ex.Message}");
            }
            finally
            {
                IsBusy = false;
            }
        }
        private void SendLocalMessage(string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                if(Messages.Count > 8)
                {
                    Messages.RemoveAt(8);
                    Messages.Insert(0, new TrackMessage
                    {
                        Message = message
                    });
                }
                else
                {
                    Messages.Insert(0,new TrackMessage
                    {
                        Message = message
                    });
                }
                
            });
        }
    }
}
