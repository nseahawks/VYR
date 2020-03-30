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
        }

        private void PointsCell_Tapped(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new NavigationPage(new PositionPage()) 
            {
                BarBackgroundColor = (Color)Application.Current.Resources["PrimaryColor"],
                BarTextColor = (Color)Application.Current.Resources["SecondaryColor"]
            });
        }

        private void LanguageCell_Tapped(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new LanguagesPopup());
        }
    }
}