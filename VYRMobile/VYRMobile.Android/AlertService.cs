using System;

using Android.App;
using Android.Content;
using Android.OS;
using Plugin.CloudFirestore;
using Plugin.CloudFirestore.Extensions;
using Plugin.LocalNotifications;

namespace VYRMobile.Droid
{
    [Service]
    class AlertService : Service
    {

        public async override void OnCreate()
        {
            //var document = CrossCloudFirestore.Current.Instance
            // .GetCollection("alerts")
            //.GetDocument("myDevice")
            //.AsObservable().Subscribe(documentChanged =>
            //{
            //    CrossLocalNotifications.Current.Show("NUEVA ALARMA", "Seahawks");

            //});

            var document = CrossCloudFirestore.Current.Instance
                .GetCollection("alerts");
            
            document.ObserveModified()
            .Subscribe(documentChange =>
            {
                var document = documentChange.Document;
                var message = $"{document.Data["Latitude"].ToString()}, {document.Data["Longitude"].ToString()}";
                CrossLocalNotifications.Current.Show("NUEVA ALARMA", "Seahawks");
            });

            document.ObserveAdded()
                .Subscribe(documentChange =>
                {
                    var document = documentChange.Document;
                    var message = $"{document.Data.ToString()}";
                    CrossLocalNotifications.Current.Show("NUEVA ALARMA", "Seahawks");
                });

            base.OnCreate();
        }

        public override IBinder OnBind(Intent intent)
        {
            return null;
        }

        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int starId)
        {

            return StartCommandResult.Sticky;
        }

    }
}