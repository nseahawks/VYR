using Firebase.Storage;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using VYRMobile.Helper;
using VYRMobile.ViewModels;
using Plugin.FilePicker;
using Syncfusion.XForms.Buttons;
using VYRMobile.Services;
using VYRMobile.Data;
using VYRMobile.Models;
using Rg.Plugins.Popup.Extensions;
using VYRMobile.Views.Popups;

namespace VYRMobile.Views
{
    public partial class Test : ContentPage
    {
        public Test()
        {
            InitializeComponent();
        }

        private void viewButton_Clicked(object sender, EventArgs e)
        {
            ApplicationUser user = new ApplicationUser()
            {
                FullName = "Ashby Hernandez",
                Id = "311f87d5-3aa9-4895-85d0-af3c8bd56f96"
            };

            App.Current.MainPage = new NavigationPage(new EditEquipmentPage(user));
            //await Navigation.PushModalAsync(new EditEquipmentPopup(user));
        }
    }
}