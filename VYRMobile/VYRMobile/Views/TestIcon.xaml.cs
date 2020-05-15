using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestIcon : ContentPage
    {
        public TestIcon()
        {
            InitializeComponent();

            NewPasswordOffClicked();
            NewPasswordOnClicked();
        }
        private void NewPasswordOffClicked()
        {
            newPasswordOff.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    img1.IsVisible = false;
                    img1.IsEnabled = false;
                    img2.IsVisible = true;
                    img2.IsEnabled = true;
                }),
                NumberOfTapsRequired = 1
            });
        }
        private void NewPasswordOnClicked()
        {
            newPasswordOn.GestureRecognizers.Add(new TapGestureRecognizer()
            {
                Command = new Command(() =>
                {
                    img2.IsVisible = false;
                    img2.IsEnabled = false;
                    img1.IsVisible = true;
                    img1.IsEnabled = true;
                }),
                NumberOfTapsRequired = 1
            });
        }
    }
}