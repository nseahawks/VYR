using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Xamarin;
using Xamarin.Forms.GoogleMaps.Android;
using Android.Content;
using FFImageLoading.Forms.Platform;
using Xamarin.Forms;
using CarouselView.FormsPlugin.Android;
using Firebase;
using Firebase.Firestore;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Firebase.Iid;

namespace VYRMobile.Droid
{
    [Activity(
        Label = "VYR-X", 
        Icon = "@drawable/vyrx", 
        Theme = "@style/MainTheme", 
        MainLauncher = true, 
        ResizeableActivity = false, 
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private static MainActivity _instance;
        public static MainActivity Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new MainActivity();

                return _instance;
            }
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            InitControls();
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(savedInstanceState);
            //Forms.SetFlags("FastRenderers_Experimental");
            Forms.SetFlags("CarouselView_Experimental");
            Forms.SetFlags("CollectionView_Experimental");
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            FirebaseApp.InitializeApp(Application.ApplicationContext);

            var platformConfig = new PlatformConfig
            {
                BitmapDescriptorFactory = new CachingNativeBitmapDescriptorFactory()
            };

            AppCenter.Start("bff38954-6dd9-4a23-a41a-13430c73bfd8",
                    typeof(Analytics), typeof(Crashes));

            FormsMaps.Init(this, savedInstanceState);
            CachedImageRenderer.Init(true);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            FormsGoogleMaps.Init(this, savedInstanceState, platformConfig);
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);
            //global::Xamarin.FormsMaps.Init(this, savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);

            FirebaseFirestore firestore = FirebaseFirestore.GetInstance(FirebaseApp.Instance);
            FirebaseFirestoreSettings settings = new FirebaseFirestoreSettings.Builder().SetTimestampsInSnapshotsEnabled(true).Build();
            firestore.FirestoreSettings = settings;
            //this.activity = Instance;

            var refreshedToken = FirebaseInstanceId.Instance.Token;

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
        public void StartAlarmService()
        {
            Intent intent = new Intent(Android.App.Application.Context.ApplicationContext, typeof(AlertService));

            Android.App.Application.Context.StartService(intent);
        }
        public void StopAlarmService()
        {
            Intent intent = new Intent(Android.App.Application.Context.ApplicationContext, typeof(AlertService));

            Android.App.Application.Context.StopService(intent);
        }
    }
}