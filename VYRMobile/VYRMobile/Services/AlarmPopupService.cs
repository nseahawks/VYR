using Plugin.CloudFirestore;
using Rg.Plugins.Popup.Extensions;
using VYRMobile.Services;
using VYRMobile.Views.Popups;

[assembly: Xamarin.Forms.Dependency(typeof(IAlarmPopup))]
namespace VYRMobile.Services
{
    public class AlarmPopupService : IAlarmPopup
    {
        public void ShowDialog(string locationName, double longitude, double latitude, string locationType)
        {
            App.Current.MainPage.Navigation.PushPopupAsync(new AlarmPopup(locationName, locationType, latitude, longitude));
        }
    }
}
