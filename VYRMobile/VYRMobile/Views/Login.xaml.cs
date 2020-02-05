using System;
using System.Threading.Tasks;
using VYRMobile.ViewModels;
using VYRMobile.Models;
using VYRMobile.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.CloudFirestore;

namespace VYRMobile
{
    public partial class Login : ContentPage
    {
        ApplicationUser usuario = new ApplicationUser();
        IdentityViewModel _log = new IdentityViewModel();

        public static readonly BindableProperty TryLoginCommandProperty =
           BindableProperty.Create(nameof(TryLoginCommand), typeof(Command),
               typeof(Login), null, BindingMode.TwoWay);

        public Login()
        {
            InitializeComponent();
            BindingContext = new IdentityViewModel();

            //email.TextChanged += RememberEmail;

            TryLoginCommand = new Command(async () => await TryLogin());
            Loginbtn.Clicked += Loginbtn_clicked;
        }

        private void Loginbtn_clicked(object sender, EventArgs e)
        {
            Loginbtn.IsEnabled = false;
            animation.IsVisible = true;
            animation.IsPlaying = true;
        }

        /*private void RememberEmail(object sender, TextChangedEventArgs e)
        {
            
        }*/

        public Command TryLoginCommand
        {
            get { return (Command)GetValue(TryLoginCommandProperty); }
            set { SetValue(TryLoginCommandProperty, value); }
        }

        async Task TryLogin()
        {
            //Application.Current.MainPage = new NavigationPage(new Utensilios());
            string _userId = await SecureStorage.GetAsync("id");
            if (App.IsUserLoggedIn)
            {
                animation.IsPlaying = false;
                await animation.FadeTo(0, 100, Easing.Linear);
                animation.IsVisible = false;
                /*animationCheck.IsVisible = true;
                await animationCheck.FadeTo(0, 0, Easing.Linear);
                await animationCheck.FadeTo(100, 50, Easing.Linear);
                animationCheck.IsPlaying = true;
                await Task.Delay(100);
                animationCheck.IsPlaying = false;
                await animationCheck.FadeTo(0, 50, Easing.Linear);*/
                

                await CrossCloudFirestore.Current.Instance
                                          .GetCollection("usersApp")
                                          .GetDocument(_userId)
                                          .UpdateDataAsync(new { LoggedIn = true});

                string user = email.Text.ToString();

                if (_log.Checked == true)
                {
                    usuario.Email = Preferences.Get(nameof(usuario.Email), user);
                    Preferences.Set(usuario.Email, user);
                }


                Application.Current.MainPage = new NavigationPage(new Utensilios());
                //await Navigation.PopAsync();
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
    }
}