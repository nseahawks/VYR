using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using VYRMobile.Helper;
using VYRMobile.Styles;
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

            var companyNameLayout = new StackLayout()
            {
                Orientation = StackOrientation.Vertical,
                Spacing = 0
            };
            var logoLayout = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                Spacing = 0
            };

            AbsoluteLayout.SetLayoutFlags(splashImage,
                AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(splashImage,
                new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            AbsoluteLayout.SetLayoutFlags(companyNameLayout, 
                AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(companyNameLayout, 
                new Rectangle(0.5, 0.95, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            logoLayout.Children.Add(label369);
            logoLayout.Children.Add(labelLabs);
            companyNameLayout.Children.Add(labelCaption);
            companyNameLayout.Children.Add(logoLayout);
            sub.Children.Add(splashImage);
            sub.Children.Add(companyNameLayout);

            BackgroundColor = Color.FromHex("#FFFFFF");
            Content = sub;
        }
        protected override async void OnAppearing() 
        {
            base.OnAppearing();

            await splashImage.ScaleTo(2.0, 1000, Easing.CubicOut);

            /*await RecoverLoginState();
            
            if (App.IsUserLoggedIn)
            {
                ICollection<ResourceDictionary> mergedDictionaries = Application.Current.Resources.MergedDictionaries;
                if (mergedDictionaries != null)
                {
                    mergedDictionaries.Clear();

                    switch (App.ApplicationUserRole)
                    {
                        default:
                            mergedDictionaries.Add(new WorkerTheme());
                            mergedDictionaries.Add(new Colors());
                            mergedDictionaries.Add(new Fonts());
                            break;
                        case "Supervisor":
                            mergedDictionaries.Add(new SupervisorTheme());
                            mergedDictionaries.Add(new Colors());
                            mergedDictionaries.Add(new Fonts());
                            break;
                    }
                }
                Application.Current.MainPage = new NavigationPage(new LoadingPage());
            }
            else
            {
                Application.Current.MainPage = new NavigationPage(new LoginPage());
            }*/

            Application.Current.MainPage = new NavigationPage(new LoginPage());
        }
        private async Task RecoverLoginState()
        {
            var loginStateData = await SecureStorage.GetAsync("isLogged");
            var tokenData = await SecureStorage.GetAsync("token");

            if (loginStateData == "True" && !string.IsNullOrEmpty(tokenData))
            {
                var handler = new JwtSecurityTokenHandler();
                var token = handler.ReadToken(tokenData) as JwtSecurityToken;

                if(token.ValidTo > DateTime.Now)
                {
                    App.ApplicationUserId = await SecureStorage.GetAsync("id");
                    App.ApplicationUserRole = await SecureStorage.GetAsync("role");
                    App.IsUserLoggedIn = Convert.ToBoolean(loginStateData);
                    App.ApplicationUserToken = token;
                    ApiHelper.Token = tokenData;
                }
            }
        }
    }
}
