using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.Views.Popups;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ConfiguracionPage : ContentPage
    {
        public ConfiguracionPage()
        {
            InitializeComponent();

            languageCell.Tapped += LanguageCell_Tapped;
            pointsCell.Tapped += PointsCell_Tapped;
            profileInfoCell.Tapped += ProfileInfoCell_Tapped;
            passwordCell.Tapped += PasswordCell_Tapped;
            FilterUserRole();
        }

        private async void ProfileInfoCell_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ProfileInfoPage())
            {
                BarBackgroundColor = (Color)Application.Current.Resources["PrimaryColor"],
                BarTextColor = (Color)Application.Current.Resources["SecondaryColor"]
            });
        }

        private async void PasswordCell_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new ChangePasswordPopup());
        }

        private async void PointsCell_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new PositionPage()) 
            {
                BarBackgroundColor = (Color)Application.Current.Resources["PrimaryColor"],
                BarTextColor = (Color)Application.Current.Resources["SecondaryColor"]
            });
        }

        private async void LanguageCell_Tapped(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new LanguagesPopup());
        }

        private void FilterUserRole()
        {
            if (App.ApplicationUserRole == "Supervisor")
            {
                pointsView.IsVisible = true;
                pointsView.IsEnabled = true;
            }
        }
    }
}