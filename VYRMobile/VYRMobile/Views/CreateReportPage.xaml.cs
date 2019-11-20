using Plugin.Media;
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
    public partial class CreateReportPage : ContentPage
    {
        public CreateReportPage()
        {
            InitializeComponent();
            btnBack.Clicked += BtnBack_clicked;
            btnAttach.Clicked += BtnAttach_clicked;
            BindingContext = new CreateReportViewModel();
        }

        private async void BtnAttach_clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("No Camera", "No camera available", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                SaveToAlbum = true,
                Name = "seahawks.png"
            });

            if (file == null)
                return;

            Image1.Source = ImageSource.FromStream(() => file.GetStream());
        }

        private void BtnBack_clicked(object sender, EventArgs e)
        {
            ((NavigationPage)this.Parent).PopAsync();
        }
    }
}