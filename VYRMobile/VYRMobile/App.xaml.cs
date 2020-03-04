﻿using System;
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
using VYRMobile.Models;
using PCLStorage;
using System.Threading.Tasks;
using VYRMobile.Helper;
using Newtonsoft.Json;
using Syncfusion.XForms.Buttons;

namespace VYRMobile
{
    public partial class App : Application
    {
        internal static bool IsUserLoggedIn = true;
        internal static string ApplicationUserId;
        internal static string ApplicationUserRole;
        internal static List<Stream> ImagesStreams = new List<Stream>();
        internal static List<string> ImagesNames = new List<string>();
        internal static List<byte[]> ImagesData = new List<byte[]>();
        internal static List<Record> Records = new List<Record>();
        internal static List<SfCheckBox> Faults = new List<SfCheckBox>();
        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("MjA0NTM1QDMxMzcyZTM0MmUzMG9ONVZEbnYzTDU4OTFaYnpTVW42YUpGME9ZSU90aXVCWi81WTZ4RHNlcDQ9");
            InitializeComponent();
            GoogleMapsApiService.Initialize(Constants.GoogleMapsApiKey);
            CreateDirectory();

            MainPage = new MainPage();
        }
        protected override void OnStart()
        {
            AppCenter.Start("bff38954-6dd9-4a23-a41a-13430c73bfd8", typeof(Push));
        }
        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }
        protected override void OnResume()
        {
            // Handle when your app resumes
        }
        private void CreateDirectory()
        {
            if (Current.Properties.ContainsKey("record"))
            {
                var json = Current.Properties["record"].ToString();
                if (json != null)
                {
                    Records = JsonConvert.DeserializeObject<List<Record>>(json);
                }
            }
        }
    }
}