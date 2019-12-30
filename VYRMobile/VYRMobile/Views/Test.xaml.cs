using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Test : ContentPage
    {
        public Test()
        {
            InitializeComponent();
            BindingContext = new PuntoViewModel();

            //btn.Clicked += Btn_Clicked;
        }

        /*private async void Btn_Clicked(object sender, EventArgs e)
        {
            await btn.FadeTo(0.5, 1000, Easing.SinIn);
            btn.IsEnabled = false;
            animation.IsVisible = true;
            await animation.FadeTo(0, 0, Easing.Linear);
            await animation.FadeTo(1, 100, Easing.Linear);
            animation.IsPlaying = true;
            await Task.Delay(5000);
            animation.IsPlaying = false;
            await animation.FadeTo(0, 500, Easing.Linear);
            animation.IsVisible = false;
            btn.IsEnabled = true;
            await btn.FadeTo(1, 1000, Easing.SinOut);

        }*/
    }
}