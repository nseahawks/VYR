using System;
using Android.App;
using Android.Content;
using Android.OS;
using Plugin.CloudFirestore;
using Plugin.CloudFirestore.Extensions;
using Plugin.LocalNotifications;
using Plugin.PushNotification;
using Rg.Plugins.Popup.Extensions;
using VYRMobile.Models;
using VYRMobile.Services;
using VYRMobile.Views.Popups;
using Xamarin.Essentials;
using Xamarin.Forms;

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
            var id = await SecureStorage.GetAsync("id");
            var document = CrossCloudFirestore.Current.Instance
                .GetCollection("usersApp")
                .GetDocument(id)
                .GetCollection("Alarms");

            //bool firstTime = true;

            //document.ObserveModified()
            //.Subscribe(documentChange =>
            //{
            //    var document = documentChange.Document;
            //    //var message = $"{document.Data["Latitude"].ToString()}, {document.Data["Longitude"].ToString()}";
            //    CrossLocalNotifications.Current.Show("NUEVA ALARMA", "Seahawks");
            //});
            document
                .ObserveAdded()
                .Subscribe(documentChange =>
                {
                    var documentC = documentChange.Document;
                    var alarmDocument = documentC.ToObject<FirestoreAlarm>();
                    
                    Xamarin.Forms.Application.Current.MainPage.Navigation.PushPopupAsync(new AlarmPopup(alarmDocument, documentC.Id.ToString()));
                    /*FirestoreAlarm alarmDocument = new FirestoreAlarm()
                    {
                        LocationName = "Seahawks",
                        Location = new GeoPoint(18.4047, -70.0328),
                        Type = "Alarm"
                    };

                    CrossLocalNotifications.Current.Show("NUEVA ALARMA", "Seahawks");

                    AlarmPopupService alarmPopupService = new AlarmPopupService();

                    alarmPopupService.ShowDialog(alarmDocument.LocationName, alarmDocument.Location, alarmDocument.Type);*/

                    //DependencyService.Get<IAlarmPopup>().ShowDialog(alarmDocument.LocationName, alarmDocument.Location, alarmDocument.Type);
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