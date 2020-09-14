using VYRMobile.Droid.Renderers;
using VYRMobile.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(StartAlarmService))]
namespace VYRMobile.Droid.Renderers
{
    public class StartAlarmService : IStartAlarmService
    {
        public void StartService()
        {
            MainActivity.Instance.StartAlarmService();
        }
        public void StopService()
        {
            MainActivity.Instance.StopAlarmService();
        }
    }
}