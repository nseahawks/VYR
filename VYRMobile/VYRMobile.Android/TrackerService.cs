using System;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Microsoft.AspNetCore.SignalR.Client;
using Plugin.CloudFirestore;
using Plugin.CloudFirestore.Extensions;
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

            var document = CrossCloudFirestore.Current.Instance
                 .GetCollection("devices");
                 //.GetDocument("myDevice");
                 //.AsObservable().Subscribe(document =>
                 //{

                 //});

            //CrossCloudFirestore.Current.Instance
            //    .GetCollection("devices")
                document.ObserveModified()
                .Subscribe(documentChange =>
                {
                    var document = documentChange.Document;
                    var message = $"{document.Data["Latitude"].ToString()}, {document.Data["Longitude"].ToString()}";
                    //DependencyService.Get<IToast>().ShortToast(message);
                });
            
            document.ObserveAdded()
                .Subscribe(documentChange =>
                {
                    var document = documentChange.Document;
                    var message = $"{document.Data.ToString()}";
                    //DependencyService.Get<IToast>().ShortToast(message);
                });

            base.OnCreate();
        }
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int starId)
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

                    var request = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromMilliseconds(1000));
                    var location = await Geolocation.GetLocationAsync(request);

                    if (location == null)
                    {
                        location = await Geolocation.GetLastKnownLocationAsync();
                    }

                    var reference = CrossCloudFirestore.Current
                        .Instance
                        .GetCollection("devices")
                        .GetDocument("myDevice");

                    await CrossCloudFirestore.Current
                        .Instance.RunTransactionAsync((transaction) =>
                        {
                            var document = transaction.GetDocument(reference);

                            transaction.UpdateData(reference, location);
                        });
                        
                    await Task.Delay(TimeSpan.FromSeconds(15));
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
}