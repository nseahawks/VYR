using System;
using System.Threading.Tasks;
using VYRMobile.ViewModels;
using VYRMobile.Models;
using VYRMobile.Views;
using Xamarin.Essentials;
using Xamarin.Forms;


using Plugin.CloudFirestore;
using Newtonsoft.Json;
using VYRMobile.Data;
using Rg.Plugins.Popup.Extensions;
using VYRMobile.Views.Popups;

namespace VYRMobile
{
    public partial class Login : ContentPage
    {
        IdentityViewModel _log = new IdentityViewModel();
        private RecordsStore _store { get; set; }

        public static readonly BindableProperty TryLoginCommandProperty =
           BindableProperty.Create(nameof(TryLoginCommand), typeof(Command),
               typeof(Login), null, BindingMode.TwoWay);

        public Login()
        {
            InitializeComponent();
            BindingContext = new IdentityViewModel();

            _store = new RecordsStore();
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
            animationView.IsVisible = true;
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
                await CrossCloudFirestore.Current.Instance
                                         .GetCollection("Users")
                                         .GetDocument(App.ApplicationUserId)
                                         .UpdateDataAsync(new { LoggedIn = true });

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
                await SecureStorage.SetAsync("records", json);

                /*await SecureStorage.SetAsync("json", json);
                var jsonn = await SecureStorage.GetAsync("json");
                if (jsonn != null)
                {
                    Records = JsonConvert.DeserializeObject<List<Record>>(json);
                }*/
                try
                {
                    await _store.AddRecordAsync(record);
                }
                catch
                {
                    await DisplayAlert("Error", "No es posible conectar con la API", "OK");
                }

                animation.IsPlaying = false;
                await animationView.FadeTo(0, 100, Easing.Linear);

                if(App.ApplicationUserRole == "Supervisor")
                {
                    Application.Current.MainPage = new NavigationPage(new Loading());
                }
                else
                {
                    Application.Current.MainPage = new NavigationPage(new Utensilios());
                }
            }
            else
            {
                await CrossCloudFirestore.Current.Instance
                                         .GetCollection("Users")
                                         .GetDocument(App.ApplicationUserId)
                                         .UpdateDataAsync(new { LoggedIn = false });
                //Login Failed
                await DisplayAlert("Login Failed", $"Verifique su usuario y contraseña", "Ok");
                await animationView.FadeTo(0, 100, Easing.Linear);
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