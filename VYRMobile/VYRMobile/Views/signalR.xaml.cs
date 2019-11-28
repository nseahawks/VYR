using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views
{
    public partial class SignalR : ContentPage
    {
        TrackViewModel vm;
        TrackViewModel VM
        {
            get => vm ?? (vm = (TrackViewModel)BindingContext);
        }

        private void UpdateLocation()
        {
            Task.Run(async () =>
            {
                var i = 0;

                while (VM.IsConnected)
                {
                    var locator = CrossGeolocator.Current;
                    locator.DesiredAccuracy = 100;
                    var position = await locator.GetPositionAsync();
                    Xamarin.Forms.Maps.Position _position = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
                    VM.TrackMessage.User = $"{i}";
                    VM.TrackMessage.Message = $"lat: {_position.Latitude}, lng: {_position.Longitude}";
                    VM.SendMessageCommand.Execute(null);
                    await Task.Delay(5000);
                    i++;
                }

            });
        }
        public SignalR()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            VM.ConnectCommand.Execute(null);
            Task.Factory.StartNew(async () =>
            {

                do
                {
                    await Task.Delay(2000);
                

                } while (!VM.IsConnected);

                if (VM.IsConnected)
                {
                    UpdateLocation();
                }
            });
           
           
        }
        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            VM.DisconnectCommand.Execute(null);
        }
    }
}