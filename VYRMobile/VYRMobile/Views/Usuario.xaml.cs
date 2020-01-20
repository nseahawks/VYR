using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views
{
    public partial class Usuario : ContentPage
    {
        public Usuario()
        {
            InitializeComponent();
            BindingContext = new OptionViewModel();
            GetUserInfo();
        }
        private async void GetUserInfo()
        {
            idLbl.Text = await SecureStorage.GetAsync("email");
        }
    }
}