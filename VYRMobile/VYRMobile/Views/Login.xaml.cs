using System;
using VYRMobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            
            Loginbtn.Clicked += Loginbtn_clicked;
        }

        private async void Loginbtn_clicked(object sender, EventArgs e)
        {
            var isValid = true;
            if (isValid)
            {
                App.IsUserLoggedIn = true;
                Navigation.InsertPageBefore(new MenuPage(), this);
                await Navigation.PopAsync();
            }
            else
            {
                //Login Failed
            }
        }
    }
}