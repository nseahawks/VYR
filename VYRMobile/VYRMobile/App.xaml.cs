using System;
using VYRMobile.Services;
using VYRMobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;
using Plugin.CloudFirestore;
using Plugin.LocalNotifications;
using Xamarin.Essentials;
using VYRMobile.ViewModels;
using System.IO;
using System.Collections.Generic;

namespace VYRMobile
{
    public partial class App : Application
    {
        internal static bool IsUserLoggedIn;
        internal static List<Stream> ImagesStreams = new List<Stream>();
        internal static List<string> ImagesNames = new List<string>();
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MTc2MjEzQDMxMzcyZTMzMmUzMFJEalRFSVZKM0g4OC9KWisvYjQxL0FMdmV3RUdzUWZlSEFwSUdCdVMwTVk9");
            InitializeComponent();
            GoogleMapsApiService.Initialize(Constants.GoogleMapsApiKey);

            MainPage = new MainPage();
        }
        protected override void OnStart()
        {
            AppCenter.Start("bff38954-6dd9-4a23-a41a-13430c73bfd8", typeof(Push));
            /*CrossCloudFirestore.Current.Instance.GetCollection("alarms")
                          .GetDocument("myDevice")
                          .AddSnapshotListener((snapshot, error) =>
                          {
                              if (IsUserLoggedIn)
                              {
                                  CrossLocalNotifications.Current.Show("ALERTA", "NSEAHAWKS");
                                  Application.Current.MainPage.DisplayAlert("ALERTA", "NSEAHAWKS", "ACEPTAR");
                                  Vibration.Vibrate();
                              }
                              
                           });*/
        }
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }
        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}