using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile
{
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        Image splashImage;
        public MainPage()
        {
            InitializeComponent();

            var sub = new AbsoluteLayout();
            splashImage = new Image 
            {
            Source="solidosh.png",
            WidthRequest = 100,
            HeightRequest = 100
            };

            AbsoluteLayout.SetLayoutFlags(splashImage,
                AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(splashImage,
                new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));

            sub.Children.Add(splashImage);

            this.BackgroundColor = Color.FromHex("#FFFFFF");
            this.Content = sub;
           
        }
        protected override async void OnAppearing() 
        {
            base.OnAppearing();

            //await splashImage.ScaleTo(1, 10);
            //await splashImage.ScaleTo(0.9, 50, Easing.Linear);
            await splashImage.ScaleTo(3.6, 2000, Easing.BounceOut);
            Application.Current.MainPage = new NavigationPage(new Login());
        }
       
        
    }
}
