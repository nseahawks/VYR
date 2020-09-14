
using Android.App;
using Android.Content;
using Android.OS;

namespace VYRMobile.Droid
{
    class AccelerometerService : Service
    {
        public override IBinder OnBind(Intent intent)
        {
            return null;
        }
    }
}