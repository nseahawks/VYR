using System;
using VYRMobile.Services;
using VYRMobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


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
            // Handle when your app starts
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
