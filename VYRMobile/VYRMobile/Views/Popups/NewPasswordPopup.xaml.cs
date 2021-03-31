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
    public partial class NewPasswordPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public NewPasswordPopup()
        {
            InitializeComponent();
        }

        private async void btnSave_Clicked(object sender, EventArgs e)
        {
            if(password.Text != confirmationPassword.Text)
            {
                wrongPassLabel.IsVisible = true;
            }
            else
            {
                App.CognitoIdentityNewPassword = password.Text;
                await Navigation.PopPopupAsync();
            }
        }
    }
}