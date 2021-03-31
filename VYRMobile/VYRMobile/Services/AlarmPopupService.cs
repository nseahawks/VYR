using VYRMobile.Services;

[assembly: Xamarin.Forms.Dependency(typeof(IAlarmPopup))]
namespace VYRMobile.Services
{
    public class AlarmPopupService : IAlarmPopup
    {
        public void ShowDialog(string locationName, string locationType)
        {
            //App.Current.MainPage.Navigation.PushPopupAsync(new AlarmPopup(locationName, position, locationType));
        }
    }
}
