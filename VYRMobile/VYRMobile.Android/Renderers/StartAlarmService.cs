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