using Plugin.CloudFirestore;
using Plugin.DeviceInfo;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            await Task.Delay(50);

            string _appId = CrossDeviceInfo.Current.Id;

            await CrossCloudFirestore.Current.Instance
                                      .GetCollection("usersApp")
                                      .GetDocument(_appId).SetDataAsync(new { Status = "CANCELED" });

            Navigation.InsertPageBefore(new Login(), Navigation.NavigationStack[0]);
            await Navigation.PopPopupAsync();
            await Navigation.PopToRootAsync();
        }
    }
}