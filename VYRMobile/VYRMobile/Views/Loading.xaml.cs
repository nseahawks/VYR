using Plugin.CloudFirestore;
using Plugin.CloudFirestore.Extensions;
using Plugin.DeviceInfo;
using System;
using System.Threading.Tasks;
using VYRMobile.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Loading : ContentPage
    {
        bool isWaiting;
        string _userId;
        public Loading()
        {
            InitializeComponent();

            isWaiting = true;
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            Charge();
            _userId = await SecureStorage.GetAsync("id");

            string repository;

            if(App.ApplicationUserRole == "Supervisor")
            {
                repository = "supervisorsApp";
            }
            else
            {
                repository = "usersApp";
            }

            var document =  CrossCloudFirestore.Current.Instance
            .GetCollection(repository)
            .GetDocument(_userId)
            .AsObservable()
            .Subscribe(document =>
            {
                var test = document.Data["Status"].ToString();

                if (test == "ACCEPTED")
                {
                    loadingMessageLabel.Text = "Inicio de sesión confirmado" + System.Environment.NewLine + "Cargando Componentes...";
                    loadingMessageLabel.TextColor = Color.FromHex("#05A66A");
                    isWaiting = false;
                    Application.Current.MainPage = new NavigationPage(new MenuPage());
                    if(App.ApplicationUserRole != "Supervisor")
                    {
                        DependencyService.Get<IStartAlarmService>().StartService();
                    }
                }
                else if (test == "CANCELED")
                {
                    loadingMessageLabel.Text = "Acceso denegado por monitoreo" + System.Environment.NewLine + "Volviendo al inicio...";
                    loadingMessageLabel.TextColor = Color.FromHex("#DD0808");
                    isWaiting = false;
                    App.IsUserLoggedIn = false;
                    Application.Current.MainPage = new NavigationPage(new Login());
                }
                else
                {
                    isWaiting = true;
                }
            });

            /*isWaiting = false;
            Application.Current.MainPage = new NavigationPage(new MenuPage());*/
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        private async void Charge()
        {
            while(isWaiting == true)
            {
                await Task.Delay(100);
            }
        }

        public class StatusU
        {
            public string Status { get; set; }
        }
    }
}