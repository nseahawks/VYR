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
        ReportViewModel _reportVM = new ReportViewModel();
        ReportsStore _store = new ReportsStore();
        public Stream imgStream;
        public string imgName;
        public CreateReportPage()
        {
            InitializeComponent();
            btnAttach.Clicked += BtnAttach_clicked;
            BindingContext = new ReportViewModel();
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
    }
}