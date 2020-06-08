using Plugin.CloudFirestore;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using VYRMobile.Services;
using VYRMobile.Views.Popups;

[assembly: Xamarin.Forms.Dependency(typeof(IAlarmPopup))]
namespace VYRMobile.Services
{
    public class AlarmPopupService : IAlarmPopup
    {
        public void ShowDialog(string locationName, GeoPoint position, string locationType)
        {
            //App.Current.MainPage.Navigation.PushPopupAsync(new AlarmPopup(locationName, position, locationType));
        }
    }
}
