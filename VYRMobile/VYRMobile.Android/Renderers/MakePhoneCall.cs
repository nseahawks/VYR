using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Net;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using VYRMobile.Droid.Renderers;
using VYRMobile.Models;
using VYRMobile.Services;
using VYRMobile.ViewModels;
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