using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private void Loginbtn_clicked(object sender, EventArgs e)
        {
            ((NavigationPage)this.Parent).PushAsync(new Dashboard());
            //((NavigationPage)this.Parent).PushAsync(new Dashboard());
        }
    }
}