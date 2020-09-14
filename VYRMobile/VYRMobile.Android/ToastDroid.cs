
using Android.App;
using Android.Widget;
using VYRMobile.Droid;
using VYRMobile.Services;

[assembly: Xamarin.Forms.Dependency(typeof(ToastDroid))]
namespace VYRMobile.Droid
{

    public class ToastDroid : IToast
    {
        public void LongToast(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }
        public void ShortToast(string message)
        {
            Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }
    }
}