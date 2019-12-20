using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Xamarin;
using Xamarin.Forms.GoogleMaps.Android;
using Android.Content;
using Plugin.LocalNotifications;
using Firebase.Firestore;
using Firebase;
using Java.Util;

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
            Xamarin.Forms.Forms.SetFlags("FastRenderers_Experimental");
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            LocalNotificationsImplementation.NotificationIconId = Resource.Drawable.seahawks;

            var platformConfig = new PlatformConfig
            {
                BitmapDescriptorFactory = new CachingNativeBitmapDescriptorFactory()
            };

            FormsMaps.Init(this, savedInstanceState);

            Intent intent = new Intent(this, typeof(TrackerService));
            StartService(intent);
            //StartService(new Intent(this, typeof(AlertService)));
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            Xamarin.FormsGoogleMaps.Init(this, savedInstanceState, platformConfig);
            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);
            global::Xamarin.FormsMaps.Init(this, savedInstanceState);
            //firestore = GetFirestore();
            //HashMap map = new HashMap();
            //map.Put("ALert", "Test");
            //DocumentReference documentReference = firestore.Collection("Alerts").Document();
            //documentReference.Set(map);

            LoadApplication(new App());
        }

        //public FirebaseFirestore GetFirestore()
        //{
        //    FirebaseFirestore firestore;

        //    var opts = new FirebaseOptions.Builder()
        //        .SetProjectId("vyrproyect-1571249849268")
        //        .SetApplicationId("vyrproyect-1571249849268")
        //        .SetApiKey("AIzaSyBwmHFGKgLdbe07Th0KGLYAOuOQcHVdNf0")
        //        .SetDatabaseUrl("https://vyrproyect-1571249849268.firebaseio.com")
        //        .SetStorageBucket("vyrproyect-1571249849268.appspot.com")
        //        .Build();

        //    var app = FirebaseApp.InitializeApp(this, opts);
        //    firestore = FirebaseFirestore.GetInstance(app);

        //    return firestore;
        //}

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);

        }
    }
}