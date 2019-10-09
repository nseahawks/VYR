using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace VYRMobile
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            // test

            var image = new Image { Source = "seahawks.png" };
            var entrar = new Button { 
                Text = "Empezar" 
            };

            entrar.Clicked += OnButtonClicked;
            var imagenLayout = new StackLayout();

            imagenLayout.Children.Add(image);
            imagenLayout.Children.Add(entrar);
            Content = imagenLayout;
        }
        private async void OnButtonClicked(object sender, EventArgs args)
        {
            //((NavigationPage)this.Parent).PushAsync(new Login());
            await Navigation.PushAsync(new Login());
        }
    }
}
