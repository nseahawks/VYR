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
    public partial class Test : TabbedPage
    {
        public Test()
        {
            InitializeComponent();
            //BindingContext = new TestViewModel();
        }

        private void testing()
        {
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new NavigationPage(new Page1()) 
            {
                BarBackgroundColor = Color.FromHex("#005EB2"),
                BarTextColor = Color.FromHex("#FFFFFF")
            };
        }
    }
}