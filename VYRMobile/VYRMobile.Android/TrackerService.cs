using System;
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
        CancellationTokenSource _cts;
        HubConnection _hub;
        string device;
        bool isConnected { get; set; }
      
        public async override void OnCreate()
        {
            device = DeviceInfo.Model + ", "+ DeviceInfo.Name;

            _hub = new HubConnectionBuilder().WithUrl("https://vyr-x.azurewebsites.net/hubs/central").Build();

         
            _hub.Closed += async (error) =>
            {
                isConnected = false;
                await Task.Delay(5000);
                await  _hub.StartAsync();
            };

            _hub.On<double,double, string>("DevicePosition", (Latitude, Longitude, Device) => {
                var finalMessage = $"{Device} says {Latitude}, {Longitude}";
                DependencyService.Get<IToast>().LongToast(finalMessage);
            });


            await _hub.StartAsync();
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
            //        if(location == null)
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

                }
            }
        }
    }
}