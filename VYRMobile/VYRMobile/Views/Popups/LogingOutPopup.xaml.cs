using Plugin.CloudFirestore;
using Plugin.DeviceInfo;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogingOutPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public LogingOutPopup()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            LogingOut();
        }
        protected override bool OnBackgroundClicked()
        {
            return false;
        }
        private async void LogingOut()
        {
            try
            {
                await CrossCloudFirestore.Current.Instance
                                          .GetCollection("Users")
                                          .GetDocument(App.ApplicationUserId)
                                          .UpdateDataAsync(new { LoggedIn = false });

                Navigation.InsertPageBefore(new LoginPage(), Navigation.NavigationStack[0]);
                await SecureStorage.SetAsync("isLogged", App.IsUserLoggedIn.ToString());
                await SecureStorage.SetAsync("token", "");
                await Navigation.PopToRootAsync();
                await Navigation.PopPopupAsync();
            }
            catch
            {
                await DisplayAlert("Error", "Ocurrió un problema al procesar la información", "Aceptar");
            }
        }
    }
}