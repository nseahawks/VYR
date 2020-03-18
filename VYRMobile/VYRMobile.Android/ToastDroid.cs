using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
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