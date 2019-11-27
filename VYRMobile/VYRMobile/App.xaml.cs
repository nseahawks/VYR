using System;
using VYRMobile.Services;
using VYRMobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Push;


namespace VYRMobile
{
    public partial class App : Application
    {
        internal static bool IsUserLoggedIn;

        public App()
        {
            InitializeComponent();
            GoogleMapsApiService.Initialize(Constants.GoogleMapsApiKey);
            //MainPage = new NavigationPage(new MenuPage());
            MainPage = new MenuPage();
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
    }
}
