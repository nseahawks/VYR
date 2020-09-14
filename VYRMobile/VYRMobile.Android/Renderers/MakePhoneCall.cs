using Android.Content;
using Android.Net;
using VYRMobile.Droid.Renderers;
using VYRMobile.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(MakePhoneCall))]
namespace VYRMobile.Droid.Renderers
{
    public class MakePhoneCall : IMakePhoneCall
    {
        public void MakeCall(string phoneNumber)
        {
            var intent = new Intent(Intent.ActionCall);
            intent.AddFlags(ActivityFlags.NewTask);
            intent.SetData(Uri.Parse("tel:" + phoneNumber));

            Android.App.Application.Context.StartActivity(intent);
        }
    }
}