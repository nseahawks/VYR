using System;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Plugin.CloudFirestore;
using Xamarin.Essentials;
using Plugin.DeviceInfo;

namespace VYRMobile.Droid
{
    [Service]
    class TrackerService : Service
    {
        CancellationTokenSource _cts;

        public bool IsConnected { get; set; }

        public override void OnCreate()
        {
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

            return StartCommandResult.Sticky;
        }

        private async void PollLocation(CancellationTokenSource cts)
        {
            var id = await SecureStorage.GetAsync("id");
            var deviceId = await SecureStorage.GetAsync("device_id");
            if (String.IsNullOrEmpty(id)) {
                await Task.Delay(TimeSpan.FromSeconds(5));
                PollLocation(cts);
            }
            else
            {
                while (!cts.IsCancellationRequested)
                {
                    try
                    {
                    //IDocumentReference reference;

                        var request = new GeolocationRequest(GeolocationAccuracy.Default, TimeSpan.FromMilliseconds(1000));
                        var location = await Geolocation.GetLocationAsync(request);

                        if (location == null)
                        {
                            location = await Geolocation.GetLastKnownLocationAsync();
                        }

                    //if (CrossDeviceInfo.IsSupported)
                    //{
                    //    string _appId = CrossDeviceInfo.Current.Id;
                    //    //var id = CrossCloudFirestore.Current.Instance
                    //    //                 .GetCollection("usersApp")
                    //    //                 .GetDocument(SecureStorage.GetAsync("id").ToString())
                    //    //                 .GetCollection("Devices")
                    //    //                 .GetDocument(_appId).Id;
                                         
                    //    await CrossCloudFirestore.Current.Instance
                    //                      .GetCollection("usersApp")
                    //                      .GetDocument(id)
                    //                      .GetCollection("Devices")
                    //                      .GetDocument(_appId)
                    //                      .GetCollection("Locations").CreateDocument()
                    //                      .SetDataAsync(location);

                    //    await SecureStorage.SetAsync("device_id", _appId);
                    //        deviceId = _appId;
                    //}
                    //else
                    //{
                    //    string _appId = new Guid().ToString();
                    //    //var id = CrossCloudFirestore.Current.Instance
                    //    //                 .GetCollection("usersApp")
                    //    //                 .GetDocument(SecureStorage.GetAsync("id").ToString())
                    //    //                 .GetCollection("Devices")
                    //    //                 .GetDocument(_appId).Id;
                    //    //.SetDataAsync(location);

                    //    await CrossCloudFirestore.Current.Instance
                    //                      .GetCollection("usersApp")
                    //                      .GetDocument(id)
                    //                      .GetCollection("Devices")
                    //                      .GetDocument(_appId)
                    //                      .GetCollection("Locations").CreateDocument()
                    //                      .SetDataAsync(location);

                    //    await SecureStorage.SetAsync("device_id", _appId);
                    //        deviceId = _appId;
                    //}

                        await CrossCloudFirestore.Current
                        .Instance
                        .GetCollection("usersApp")
                        .GetDocument(id)
                        .GetCollection("Devices")
                        .GetDocument(deviceId)
                        .GetCollection("Locations").AddDocumentAsync(location);

                    //await CrossCloudFirestore.Current
                    //    .Instance.RunTransactionAsync((transaction) =>
                    //    {
                    //        var document = transaction.GetDocument(reference);

                    //        transaction.UpdateData(reference, location);
                    //    });
                        
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
}