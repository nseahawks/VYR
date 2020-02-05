using System;
using System.Threading.Tasks;
using VYRMobile.ViewModels;
using VYRMobile.Models;
using VYRMobile.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
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
            if (App.IsUserLoggedIn)
            {
                animation.IsPlaying = false;
                await animation.FadeTo(0, 100, Easing.Linear);
                animation.IsVisible = false;

                if (_log.Checked == true)
                {
                    string emailText = email.Text.ToString();

                    await SecureStorage.SetAsync("EmailRemembered", emailText);
                }

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

                Application.Current.MainPage = new NavigationPage(new Utensilios());
            }
            else
            {
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