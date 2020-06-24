using Plugin.CloudFirestore;
using Plugin.Media;
using Rg.Plugins.Popup.Extensions;
using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.Controls;
using VYRMobile.Data;
using VYRMobile.Helper;
using VYRMobile.Models;
using VYRMobile.Services;
using VYRMobile.ViewModels;
using VYRMobile.Views.Popups;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestIcon : ContentPage
    {
        public TestIcon()
        {
            InitializeComponent();

            BindingContext = new SupervisionViewModel();
        }

        private async void btnReporte_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new CreateReportPage()));
        }
    }
}