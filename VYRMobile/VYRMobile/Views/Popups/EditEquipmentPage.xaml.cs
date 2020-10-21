using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.Models;
using VYRMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EditEquipmentPage : ContentPage
    {
        private static string userId;
        public EditEquipmentPage(ApplicationUser user)
        {
            InitializeComponent();

            BindingContext = EquipmentViewModel.CustomInstance;
            EquipmentViewModel.CustomInstance.LoadData(user.Id);

            userFullnameLabel.Text = user.FullName;
            userId = "Id-4646464";
        }

        private async void saveChangesButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushPopupAsync(new LoadingPopup("Guardando"));

            await Task.Delay(1000);

            //var isSuccess = await EquipmentViewModel.CustomInstance.UpdateEquipment(userId);

            await Navigation.PopPopupAsync();

            await Navigation.PopAsync();
            
        }

        private void optionSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            EquipmentViewModel.CustomInstance.EnableOrDisableCommentaryBox();
        }
    }
}