using System;
using VYRMobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile
{


    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            
            Loginbtn.Clicked += Loginbtn_clicked;
        }

        private void Loginbtn_clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new Utensilios());
            /*var isValid = true;
            if (isValid)
            {
                App.IsUserLoggedIn = true;
                Navigation.InsertPageBefore(new Utensilios(), this);
                await Navigation.PopAsync();
            }
            else
            {
                //Login Failed
            }*/
        }
    }
}