/*using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Microsoft.AspNetCore.SignalR.Client;
using VYRMobile.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VYRMobile.Droid
{
    [Service]
    class TrackerService : Service
    {
        SensorSpeed speed = SensorSpeed.Default;
        CancellationTokenSource _cts;
        HubConnection _hub;
        private AccelerometerData data;
        string device;
        private bool isConnected;
        public bool IsConnected 
        {
            get => isConnected; 
            set => isConnected = value; 
        }
      
        public async override void OnCreate()
        {
            device = DeviceInfo.Name;
            ToggleAccelerometer();
            Accelerometer.ReadingChanged += Accelerometer_ReadingChanged;
            _hub = new HubConnectionBuilder().WithUrl("https://vyr-x.azurewebsites.net/hubs/central").Build();

         
            _hub.Closed += async (error) =>
            {
                IsConnected = false;
                await Task.Delay(5000);
                await  _hub.StartAsync();
            };

            _hub.On<double, double, string>("DevicePosition", (Latitude, Longitude, Device) =>
            {
                var finalMessage = $"{Device} Location: {Latitude}, {Longitude}, Speed: {data}";
                DependencyService.Get<IToast>().ShortToast(finalMessage);
            });


            await _hub.StartAsync();
            IsConnected =  _hub.StartAsync().IsCompleted;
            base.OnCreate();
        }
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand (Intent intent, StartCommandFlags flags, int starId)
        {
            _cts = new CancellationTokenSource();
            PollLocation(_cts);

            //Task.Run(async () =>
            //{
            //    try
            //    {
            //        var location = await Geolocation.GetLastKnownLocationAsync();
            //        if (location == null)
            //        {
            //            var request = new GeolocationRequest(GeolocationAccuracy.Best);
            //            location = await Geolocation.GetLocationAsync(request);
            //        }


            //        await _hub.InvokeAsync("SendLocation", location.Latitude, location.Longitude, device);
            //    }
            //    catch (System.OperationCanceledException)
            //    {

            //    }

            //}, _cts.Token);




            return StartCommandResult.Sticky;
        }

        private async void PollLocation(CancellationTokenSource cts)
        {
            while (!cts.IsCancellationRequested)
            {
                try
                {
                    var location = await Geolocation.GetLastKnownLocationAsync();
                    if (location == null)
                    {
                        var request = new GeolocationRequest(GeolocationAccuracy.Best);
                        location = await Geolocation.GetLocationAsync(request);
                    }
                    await _hub.InvokeAsync("SendLocationAsync", location.Latitude, location.Longitude, device);
                    await Task.Delay(TimeSpan.FromSeconds(5));
                }
                catch (System.OperationCanceledException)
                {
                    return;
                }
            }
        }

        void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            data = e.Reading;
            
            // Process Acceleration X, Y, and Z
        }

        public void ToggleAccelerometer()
        {
            try
            {
                if (Accelerometer.IsMonitoring)
                    Accelerometer.Stop();
                else
                    Accelerometer.Start(speed);
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
            }
            catch (Exception ex)
            {
                // Other error has occurred.
            }
        }
    }
}*/