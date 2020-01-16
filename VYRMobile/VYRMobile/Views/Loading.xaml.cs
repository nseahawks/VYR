using Plugin.CloudFirestore;
using Plugin.CloudFirestore.Extensions;
using Plugin.DeviceInfo;
using System;
using System.Threading.Tasks;
using VYRMobile.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Loading : ContentPage
    {

        bool isWaiting;
        string _appId;
        public Loading()
        {
            _appId = CrossDeviceInfo.Current.Id;
            InitializeComponent();
            var document = CrossCloudFirestore.Current.Instance
                 .GetCollection("usersApp")
            .GetDocument(_appId)
            .AsObservable().Subscribe(document =>
            {
                var test = document.Data["Status"].ToString();

                if(test == "ACCEPTED")
                {
                    isWaiting = false;
                    Application.Current.MainPage = new NavigationPage(new MenuPage());

                }else if(test == "CANCELED"){
                    isWaiting = false;
                    App.IsUserLoggedIn = false;
                    Application.Current.MainPage = new NavigationPage(new Login());

                }
                else
                {
                    isWaiting = true;
                }
            });

            isWaiting = true;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
           
            Charge();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            
        }

        private async void Charge()
        {



         

            while (isWaiting)
            {
                //var result = CheckStatus().ToString();

                //if(result == "ACCEPTED")
                //{
                //    isWaiting = false;
                //}
                await Task.Delay(100);
            }
          
            //Application.Current.MainPage = new NavigationPage(new MenuPage());
        }

        public class StatusU
        {
            public string Status { get; set; }
        }
    }
}