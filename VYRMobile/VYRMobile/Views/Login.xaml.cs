﻿using System;
using System.Threading.Tasks;
using VYRMobile.ViewModels;
using VYRMobile.Models;
using VYRMobile.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.CloudFirestore;
using Newtonsoft.Json;

namespace VYRMobile
{
    public partial class Login : ContentPage
    {
        IdentityViewModel _log = new IdentityViewModel();

        public static readonly BindableProperty TryLoginCommandProperty =
           BindableProperty.Create(nameof(TryLoginCommand), typeof(Command),
               typeof(Login), null, BindingMode.TwoWay);

        public Login()
        {
            InitializeComponent();
            BindingContext = new IdentityViewModel();

            TryLoginCommand = new Command(async () => await TryLogin());
            Loginbtn.Clicked += Loginbtn_clicked;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            RememberUser();
        }

        private void Loginbtn_clicked(object sender, EventArgs e)
        {
            Loginbtn.IsEnabled = false;
            animation.IsVisible = true;
            animation.IsPlaying = true;
        }

        public Command TryLoginCommand
        {
            get { return (Command)GetValue(TryLoginCommandProperty); }
            set { SetValue(TryLoginCommandProperty, value); }
        }

        async Task TryLogin()
        {
            string _userId = await SecureStorage.GetAsync("id");
            if (App.IsUserLoggedIn)
            {
                animation.IsPlaying = false;
                await animation.FadeTo(0, 100, Easing.Linear);
                animation.IsVisible = false;

                /*CrossCloudFirestore.Current.Instance
                                          .GetCollection("usersApp")
                                          .GetDocument(_userId);

                await CrossCloudFirestore.Current.Instance
                                          .GetCollection("usersApp")
                                          .GetDocument(_userId)
                                          .UpdateDataAsync(new { LoggedIn = true });*/

                string user = email.Text.ToString();
                await SecureStorage.SetAsync("EmailRemembered", user);

                var record = new Record()
                {
                    UserId = await SecureStorage.GetAsync("id"),
                    Type = "Inicio de sesión",
                    RecordType = Record.RecordTypes.LogIn,
                    Owner = "Yo",
                    Date = DateTime.Now,
                    Icon = "outer.png"
                };

                App.Records.Add(record);
                var Records = App.Records;
                var json = JsonConvert.SerializeObject(Records);
                Application.Current.Properties["record"] = json;


                Application.Current.MainPage = new NavigationPage(new Loading());
            }
            else
            {
                await CrossCloudFirestore.Current.Instance
                                         .GetCollection("usersApp")
                                         .GetDocument(_userId)
                                         .UpdateDataAsync(new { LoggedIn = false });
                //Login Failed
                await DisplayAlert("Login Failed", $"Verifique su usuario y contraseña", "Ok");
                await animation.FadeTo(0, 100, Easing.Linear);
                animation.IsVisible = false;
                Loginbtn.IsEnabled = true;
            }
            
        }
        private async void RememberUser()
        {
            if(_log.Checked)
            {
                email.Text = await SecureStorage.GetAsync("EmailRemembered");
            }
        }
    }
}