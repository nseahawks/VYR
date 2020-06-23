using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

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
                Source="vyrx.png",
                WidthRequest = 100,
                HeightRequest = 100
            };
            /*imageName = new Image
            {
                Source = "vyr.png",
                WidthRequest = 100,
                HeightRequest = 100
            };*/

            AbsoluteLayout.SetLayoutFlags(splashImage,
                AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(splashImage,
                new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            /*AbsoluteLayout.SetLayoutFlags(imageName,
                AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(splashImage,
                new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));*/

            sub.Children.Add(splashImage);
            //sub.Children.Add(imageName);

            this.BackgroundColor = Color.FromHex("#FFFFFF");
            this.Content = sub;
           
        }
        protected override async void OnAppearing() 
        {
            base.OnAppearing();

            await splashImage.ScaleTo(2.0, 1000, Easing.CubicOut);
            //await ImageAppearing(imageName);
            Application.Current.MainPage = new NavigationPage(new Login());
        }
        private async Task ImageAppearing(Image image)
        {
            uint duration = 300;

            await image.FadeTo(0, 0);

            var animation = new Animation();

            animation.WithConcurrent((f) => image.Opacity = f, 0, 1, Easing.CubicOut);

            animation.WithConcurrent(
              (f) => image.TranslationY = f,
              image.TranslationY + 100, 0,
              Easing.CubicOut, 0, 1);

            image.Animate("FadeIn", animation, 16, Convert.ToUInt32(duration));
        }
    }
}
