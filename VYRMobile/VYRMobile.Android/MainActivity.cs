using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android.Widget;
using Android.Gms.Common;

using Xamarin;
using Xamarin.Forms.GoogleMaps.Android;
using Android.Content;
using Plugin.LocalNotifications;
using Plugin.Messaging;

namespace VYRMobile.Droid
{
    [Activity(Label = "369Monitoreo", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(savedInstanceState);
            //Xamarin.Forms.Forms.SetFlags("FastRenderers_Experimental");
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            var platformConfig = new PlatformConfig
            {
                BitmapDescriptorFactory = new CachingNativeBitmapDescriptorFactory()
            };

            FormsMaps.Init(this, savedInstanceState);
            Intent intent = new Intent(this, typeof(TrackerService));
            StartService(intent);

            CheckForGoogleServices();
            
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Xamarin.FormsGoogleMaps.Init(this, savedInstanceState, platformConfig);
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);
            global::Xamarin.FormsMaps.Init(this, savedInstanceState);
            LoadApplication(new App());

            LocalNotificationsImplementation.NotificationIconId = Resource.Drawable.seahawks;
            CrossMessaging.Current.Settings().Phone.AutoDial = true;
        }
        public bool CheckForGoogleServices()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    Toast.MakeText(this, GoogleApiAvailability.Instance.GetErrorString(resultCode), ToastLength.Long);
                }
                else
                {
                    Toast.MakeText(this, "This device does not support Google Play Services", ToastLength.Long);
                }
                return false;
            }
            return true;
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        }
        
    }
}