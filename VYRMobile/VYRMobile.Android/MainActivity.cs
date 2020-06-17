using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Xamarin;
using Xamarin.Forms.GoogleMaps.Android;
using Android.Content;
using Plugin.LocalNotifications;
using FFImageLoading.Forms.Platform;
using Xamarin.Forms;
using CarouselView.FormsPlugin.Android;
using Firebase;
using Firebase.Firestore;
using Android.Net;
using Plugin.PushNotification;

namespace VYRMobile.Droid
{
    [Activity(Label = "VYR-X", Icon = "@drawable/vyrx", Theme = "@style/MainTheme", MainLauncher = true, ResizeableActivity = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            InitControls();
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(savedInstanceState);
            Forms.SetFlags("FastRenderers_Experimental");
            Forms.SetFlags("CarouselView_Experimental");
            Forms.SetFlags("CollectionView_Experimental");
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            FirebaseApp.InitializeApp(Application.ApplicationContext);

            LocalNotificationsImplementation.NotificationIconId = Resource.Drawable.seahawks;

            var platformConfig = new PlatformConfig
            {
                BitmapDescriptorFactory = new CachingNativeBitmapDescriptorFactory()
            };

            FormsMaps.Init(this, savedInstanceState);
            CachedImageRenderer.Init(true);

            //Intent intent = new Intent(this, typeof(AlertService));

            //StartService(intent);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            FormsGoogleMaps.Init(this, savedInstanceState, platformConfig);
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);
            global::Xamarin.FormsMaps.Init(this, savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            PushNotificationManager.ProcessIntent(this, Intent);

            FirebaseFirestore firestore = FirebaseFirestore.GetInstance(FirebaseApp.Instance);
            FirebaseFirestoreSettings settings = new FirebaseFirestoreSettings.Builder().SetTimestampsInSnapshotsEnabled(true).Build();
            firestore.FirestoreSettings = settings;

            LoadApplication(new App());
        }

        private void InitControls()
        {
            CarouselViewRenderer.Init();
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        }
        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
            PushNotificationManager.ProcessIntent(this, intent);
        }
    }
}