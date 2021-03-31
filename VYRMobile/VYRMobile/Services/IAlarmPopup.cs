using Plugin.CloudFirestore;

namespace VYRMobile.Services
{
    public interface IAlarmPopup
    {
        void ShowDialog(string locationName, double longitude, double latitude, string locationType);
    }
}
