using Plugin.CloudFirestore;

namespace VYRMobile.Services
{
    public interface IAlarmPopup
    {
        void ShowDialog(string locationName, GeoPoint position, string locationType);
    }
}
