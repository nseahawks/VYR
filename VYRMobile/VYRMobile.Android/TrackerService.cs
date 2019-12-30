using System;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Plugin.CloudFirestore;
using Plugin.CloudFirestore.Extensions;
using Xamarin.Essentials;

namespace VYRMobile.Droid
{
    [Service]
    class TrackerService : Service
    {
        CancellationTokenSource _cts;

        string device;

        public bool IsConnected { get; set; }

        public async override void OnCreate()
        {
            device = DeviceInfo.Name;
            


            //var document = CrossCloudFirestore.Current.Instance
            //     .GetCollection("devices");
                 //.GetDocument("myDevice");
                 //.AsObservable().Subscribe(document =>
                 //{
                 
                 //});

            //CrossCloudFirestore.Current.Instance
            //    .GetCollection("devices")
            //    document.ObserveModified()
            //    .Subscribe(documentChange =>
            //    {
            //        var documents = documentChange.Document;
            //        var message = $"{documents.Data["Latitude"].ToString()}, {documents.Data["Longitude"].ToString()}";
            //        //DependencyService.Get<IToast>().ShortToast(message);
            //    });
            
            //document.ObserveAdded()
            //    .Subscribe(documentChange =>
            //    {
            //        var documents = documentChange.Document;
            //        var message = $"{document.Data.ToString()}";
            //        //DependencyService.Get<IToast>().ShortToast(message);
            //    });

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
                    IDocumentReference reference;
                    var deviceId = await SecureStorage.GetAsync("device_id");
                    

                    var request = new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromMilliseconds(1000));
                    var location = await Geolocation.GetLocationAsync(request);

                    if (location == null)
                    {
                        location = await Geolocation.GetLastKnownLocationAsync();
                    }


                    if (deviceId == null)
                    {
                        //var id = new Guid().ToString();
                        //await CrossCloudFirestore.Current.Instance.GetCollection("devices")
                        //                                            .AddDocumentAsync(location);
                        //.CreateDocument().SetDataAsync(location);

                        var id = CrossCloudFirestore.Current.Instance
                                          .GetCollection("devices")
                                          .CreateDocument().Id;
                                        //.SetDataAsync(location);
                        
                        await CrossCloudFirestore.Current.Instance
                                          .GetCollection("devices")
                                          .GetDocument(id)
                                        .SetDataAsync(location);

                        await SecureStorage.SetAsync("device_id", id);

                    }
               
                    reference = CrossCloudFirestore.Current
                    .Instance
                    .GetCollection("devices").GetDocument(await SecureStorage.GetAsync("device_id"));

                    await CrossCloudFirestore.Current
                        .Instance.RunTransactionAsync((transaction) =>
                        {
                            var document = transaction.GetDocument(reference);

                            transaction.UpdateData(reference, location);
                        });
                        
                    await Task.Delay(TimeSpan.FromSeconds(5));
                }
                catch (System.OperationCanceledException)
                {
                    return;
                }
            }
        }

   
    }
}