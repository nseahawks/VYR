using Plugin.Media;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using VYRMobile.Models;
using VYRMobile.ViewModels;
using Xamarin.Forms;
using VYRMobile.Data;
using Xamarin.Essentials;

namespace VYRMobile.Views
{
    public partial class CreateReportPage : ContentPage
    {
        public Command EnableCommand { get; set; }
        public Command DisableCommand { get; set; }
        public Stream imgStream;
        public string imgName;
        public CreateReportPage()
        {
            InitializeComponent();
            btnAttach.Clicked += BtnAttach_clicked;
            BindingContext = new ReportViewModel();

            EnableCommand = new Command(async () => await EnableButtons());
            DisableCommand = new Command(async () => await DisableButtons());
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

            imgStream = file.GetStream();
            imgName = Path.GetFileName(file.Path);

            App.ImageStream = imgStream;
            App.ImageName = imgName;

            Image1.Source = ImageSource.FromStream(() => file.GetStream());
        }

        private async Task EnableButtons()
        {
            await Task.Delay(0);
            btnEnviar.IsEnabled = true;
            btnAttach.IsEnabled = true;
        }
        private async Task DisableButtons()
        {
            await Task.Delay(0);
            btnEnviar.IsEnabled = false;
            btnAttach.IsEnabled = false;
        }
    }
}