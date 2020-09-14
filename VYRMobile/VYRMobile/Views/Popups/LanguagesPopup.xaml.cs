using Rg.Plugins.Popup.Extensions;
using System;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LanguagesPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public LanguagesPopup()
        {
            InitializeComponent();

            okButton.Clicked += OkButton_Clicked;
        }

        private void OkButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PopPopupAsync();
        }
    }
}