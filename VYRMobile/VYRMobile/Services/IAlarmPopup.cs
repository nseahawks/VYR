using Plugin.CloudFirestore;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using VYRMobile.Views.Popups;

namespace VYRMobile.Services
{
    public interface IAlarmPopup
    {
        void ShowDialog(string locationName, GeoPoint position, string locationType);
        /*{
            App.Current.MainPage.Navigation.PushPopupAsync(new AlarmPopup(locationName, position, locationType));
        }*/
    }
}
