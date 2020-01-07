using Plugin.Media;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using VYRMobile.Models;
using VYRMobile.ViewModels;
using Xamarin.Forms;

namespace VYRMobile.Views
{
    public partial class CreateReportPage : ContentPage
    {
        ReportViewModel vm = new ReportViewModel();
        Report report = new Report();
        public CreateReportPage()
        {
            InitializeComponent();
            btnAttach.Clicked += BtnAttach_clicked;
            BindingContext = new ReportViewModel();
            //btnEnviar.Clicked += BtnEnviar_Clicked;
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
    }
}