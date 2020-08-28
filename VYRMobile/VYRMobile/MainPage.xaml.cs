using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using VYRMobile.Helper;
using VYRMobile.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VYRMobile
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        Image splashImage;
        public MainPage()
        {
            InitializeComponent();

            var sub = new AbsoluteLayout();
            splashImage = new Image 
            {
                Source="vyrx.png",
                WidthRequest = 100,
                HeightRequest = 100
            };

            AbsoluteLayout.SetLayoutFlags(splashImage,
                AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(splashImage,
                new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            sub.Children.Add(splashImage);

            BackgroundColor = Color.FromHex("#FFFFFF");
            Content = sub;
           
        }
        protected override async void OnAppearing() 
        {
            base.OnAppearing();

            await splashImage.ScaleTo(2.0, 1000, Easing.CubicOut);

            await RecoverLoginState();
            
            if (App.IsUserLoggedIn)
            {
                Application.Current.MainPage = new NavigationPage(new LoadingPage());
            }
            else
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }
        }
        private async Task RecoverLoginState()
        {
            var loginStateData = await SecureStorage.GetAsync("isLogged");
            var tokenData = await SecureStorage.GetAsync("token");

            if (loginStateData == "true" && !string.IsNullOrEmpty(tokenData))
            {
                JwtSecurityToken token = JsonConvert.DeserializeObject<JwtSecurityToken>(tokenData);

                if(token.ValidTo > DateTime.Now)
                {
                    App.ApplicationUserId = await SecureStorage.GetAsync("id");
                    App.ApplicationUserRole = await SecureStorage.GetAsync("role");
                    App.IsUserLoggedIn = Convert.ToBoolean(loginStateData);
                    App.ApplicationUserToken = token;
                    ApiHelper.Token = token.Id;
                }
            }
        }
    }
}
