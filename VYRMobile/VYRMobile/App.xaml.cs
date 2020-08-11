using VYRMobile.Services;
using Xamarin.Forms;
using System.IO;
using System.Collections.Generic;
using VYRMobile.Models;
using Newtonsoft.Json;
using Syncfusion.XForms.Buttons;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using Microsoft.AppCenter.Crashes;
using System;

namespace VYRMobile
{
    public partial class App : Application
    {
        internal bool isUserConnected => Connectivity.NetworkAccess == NetworkAccess.Internet;
        internal static bool IsUserLoggedIn = true;
        internal static bool IsEquipmentValitated = false;
        internal static bool HasAppCrashed = false;
        internal static string ApplicationUserId;
        internal static string ApplicationUserRole;
        internal static string AntennaId;
        internal static string ReviewedUserId;
        internal static string AlarmDocumentId;
        internal static ApplicationUser WorkerOnReview;
        internal static FirestoreAlarm Alarm;
        internal static List<Antena> UserLocations = new List<Antena>();
        internal static List<Stream> ImagesStreams = new List<Stream>();
        internal static List<string> ImagesNames = new List<string>();
        internal static List<string> ChipsNames = new List<string>();
        internal static List<Record> Records = new List<Record>();
        internal static List<SfCheckBox> Faults = new List<SfCheckBox>();
        internal static List<Calculation> Calculations = new List<Calculation>();
        internal static List<ApplicationUser> Workers = new List<ApplicationUser>();
        internal static ObservableCollection<ApplicationUser> ExchangeableWorkers = new ObservableCollection<ApplicationUser>();

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(Constants.SyncfusionLicenseKey);
            InitializeComponent();
            GoogleMapsApiService.Initialize(Constants.GoogleMapsApiKey);
            CreateDirectory();
            //SaveCrashReport();

            MainPage = new MainPage();
        }
        protected override void OnStart()
        {
            //AppCenter.Start("bff38954-6dd9-4a23-a41a-13430c73bfd8", typeof(Push));

            /*AppCenter.Start("android=bff38954-6dd9-4a23-a41a-13430c73bfd8;",
                              typeof(Analytics), typeof(Crashes));*/
        }
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }
        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        private async void CreateDirectory()
        {
            var json = await SecureStorage.GetAsync("records");
            if (json != null)
            {
                Records = JsonConvert.DeserializeObject<List<Record>>(json);
            }
        }
        private async void SaveCrashReport()
        {
            try
            {
                /*bool hasStopped = await Crashes.HasCrashedInLastSessionAsync();

                if (hasStopped)
                {*/
                    ErrorReport crashReport = await Crashes.GetLastSessionCrashReportAsync();

                    Report crashDetails = new Report()
                    {
                        Title = "Reporte de error",
                        Description = crashReport.AndroidDetails.ToString(),
                        Created = DateTime.Now
                    };

                    await SecureStorage.SetAsync("crashReportData", JsonConvert.SerializeObject(crashDetails));
                    HasAppCrashed = true;
                //}
            }
            catch (Exception ex)
            {
                ex.ToString();
            }
        }
    }
}