using Newtonsoft.Json;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Threading.Tasks;
using VYRMobile.Data;
using VYRMobile.Models;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogingOutPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        private RecordsStore _store { get; set; }
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
                var record = new Record()
                {
                    UserId = await SecureStorage.GetAsync("id"),
                    Type = "Cierre de sesión",
                    RecordType = Record.RecordTypes.LogOut,
                    Owner = "Usuario",
                    Date = DateTime.Now,
                    Icon = "logout_colored.png"
                };

                App.Records.Add(record);
                var Records = App.Records;
                var json = JsonConvert.SerializeObject(Records);
                await SecureStorage.SetAsync("records", json);

                await Task.Delay(500);

                /*await _store.AddRecordAsync(record);

                await CrossCloudFirestore.Current.Instance
                                          .GetCollection("Users")
                                          .GetDocument(App.ApplicationUserId)
                                          .UpdateDataAsync(new { LoggedIn = false });*/

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