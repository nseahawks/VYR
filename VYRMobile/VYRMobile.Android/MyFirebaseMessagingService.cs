using Android.App;
using Android.Content;
using Firebase.Messaging;
using Rg.Plugins.Popup.Extensions;
using System.Collections.Generic;
using VYRMobile.Views.Popups;

namespace XamTabBadge.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        public MyFirebaseMessagingService()
        {

        }
        public override void OnMessageReceived(RemoteMessage message)
        {
            base.OnMessageReceived(message);
            //new NotificationHelper().CreateNotification(message.GetNotification().Title, message.GetNotification().Body);

            var body = message.GetNotification().Body;
            var data = message.Data;

            ConsumeData(body, data);
        }
        private void ConsumeData(string messageBody, IDictionary<string, string> data)
        {
            string name = "";
            string type = "";
            string latitude = "";
            string longitude = "";
            foreach (KeyValuePair<string, string> pair in data)
            {
                switch (pair.Key)
                {
                    default:
                        break;
                    case "name":
                        name = pair.Value;
                        break;
                    case "type":
                        type = pair.Value;
                        break;
                    case "latitude":
                        latitude = pair.Value;
                        break;
                    case "longitude":
                        longitude = pair.Value;
                        break;
                }
                var key = pair.Key;
                var value = pair.Value;
            }

            Xamarin.Forms.Application.Current.MainPage.Navigation.PushPopupAsync(new AlarmPopup(name, type, double.Parse(latitude, System.Globalization.CultureInfo.InvariantCulture), double.Parse(longitude, System.Globalization.CultureInfo.InvariantCulture)));
        }
    }
}