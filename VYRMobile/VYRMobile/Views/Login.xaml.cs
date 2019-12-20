﻿using System;
using System.Threading.Tasks;
using VYRMobile.ViewModels;
using VYRMobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile
{
    public partial class Login : ContentPage
    {
        public static readonly BindableProperty TryLoginCommandProperty =
           BindableProperty.Create(nameof(TryLoginCommand), typeof(Command),
               typeof(Mapa2), null, BindingMode.TwoWay);

        private bool rememberMe = false;
        public bool RememberMe
        {
            get => rememberMe;

            set
            {
                rememberMe = value;
                OnPropertyChanged(nameof(RememberMe));
            }
        }

        public Login()
        {
            InitializeComponent();
            BindingContext = new IdentityViewModel();

            TryLoginCommand = new Command(async () => await TryLogin());
            //Loginbtn.Clicked += Loginbtn_clicked;
        }

        public Command TryLoginCommand
        {
            get { return (Command)GetValue(TryLoginCommandProperty); }
            set { SetValue(TryLoginCommandProperty, value); }
        }

        async Task TryLogin()
        {
            //Application.Current.MainPage = new NavigationPage(new Utensilios());


            if (App.IsUserLoggedIn)
            {
                Application.Current.MainPage = new NavigationPage(new Utensilios());
                //await Navigation.PopAsync();
            }
            else
            {
                //Login Failed
                await DisplayAlert("Login Failed", $"Verifique su usuario y contraseña", "Ok");
            }
        }
    }
}