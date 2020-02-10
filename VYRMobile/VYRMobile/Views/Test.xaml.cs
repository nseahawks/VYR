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
using Xamarin.Essentials;
using VYRMobile.Models;
using Newtonsoft.Json;
using System.Net.Http;

namespace VYRMobile.Views
{
    //[XamlCompilation(XamlCompilationOptions.Compile]
    public partial class Test : ContentPage
    {
        string localPath;
        const string recordItems = "RecordItems";
        public Test()
        {
            InitializeComponent();
            BindingContext = new CallViewModel();
        }

        private void switch_Toggled(object sender, ToggledEventArgs e)
        {

        }
    }
}