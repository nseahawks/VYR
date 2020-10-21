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
    public partial class LoginPage : ContentPage
    {
        private RecordsStore _store { get; set; }

        public static readonly BindableProperty TryLoginCommandProperty =
           BindableProperty.Create(nameof(TryLoginCommand), typeof(Command),
               typeof(LoginPage), null, BindingMode.TwoWay);

        public LoginPage()
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
            Loginbtn.Text = "Conectando...";
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
                var record = new Record()
                {
                    UserId = await SecureStorage.GetAsync("id"),
                    Type = "Inicio de sesión",
                    RecordType = Record.RecordTypes.LogIn,
                    Owner = "Usuario",
                    Date = DateTime.Now,
                    Icon = "login.png"
                };

                /*try
                {
                    await _store.AddRecordAsync(record);
                    await CrossCloudFirestore.Current.Instance
                                             .GetCollection("Users")
                                             .GetDocument(App.ApplicationUserId)
                                             .UpdateDataAsync(new { LoggedIn = true });
                }
                catch
                {
                    await DisplayAlert("Error", "No es posible conectar con el servidor", "Aceptar");
                    animation.IsPlaying = false;
                    await animationView.FadeTo(0, 100, Easing.Linear);
                    Loginbtn.IsEnabled = true;
                    Loginbtn.Text = "Iniciar sesión";

                    return;
                }*/

                App.Records.Add(record);
                var Records = App.Records;
                var json = JsonConvert.SerializeObject(Records);
                await SecureStorage.SetAsync("records", json);
            
                await SecureStorage.SetAsync("EmailRemembered", email.Text.ToString());

                /*await SecureStorage.SetAsync("json", json);
                var jsonn = await SecureStorage.GetAsync("json");
                if (jsonn != null)
                {
                    Records = JsonConvert.DeserializeObject<List<Record>>(json);
                }*/

                animation.IsPlaying = false;
                await animationView.FadeTo(0, 100, Easing.Linear);

                if(App.ApplicationUserRole == "Supervisor")
                {
                    Application.Current.MainPage = new NavigationPage(new LoadingPage());
                }
                else
                {
                    Application.Current.MainPage = new NavigationPage(new EquipmentPage());
                }
            }
            else
            {
                //Login Failed
                await DisplayAlert("Login Failed", $"Verifique su usuario y contraseña", "Ok");
                await animationView.FadeTo(0, 100, Easing.Linear);
                animation.IsVisible = false;
                Loginbtn.IsEnabled = true;
                Loginbtn.Text = "Iniciar sesión";
            }
        }
        private async void RememberUser()
        {
            if(rememberMeCheckbox.IsChecked)
            {
                email.Text = await SecureStorage.GetAsync("EmailRemembered");
            }
        }
    }
}