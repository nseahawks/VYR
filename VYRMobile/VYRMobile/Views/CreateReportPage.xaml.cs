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
        private Stream imgStream;
        private string imgName;
        private bool IsBacking;
        public CreateReportPage()
        {
            InitializeComponent();
            btnAttach.Clicked += BtnAttach_clicked;
            BindingContext = new ReportViewModel();

            EnableCommand = new Command(async () => await EnableButtons());
            DisableCommand = new Command(async () => await DisableButtons());
        }
        protected override bool OnBackButtonPressed()
        {
            bool action = IsBackingResponse().IsCanceled;
            if (action)
            {
                Navigation.PopModalAsync();
            }

            return action;
        }
        private async Task<bool> IsBackingResponse()
        {
            IsBacking = await DisplayAlert("Confirmación", "¿Está seguro que desea salir? (Los cambios no serán guardados)", "ACEPTAR", "CANCELAR");
            
            return IsBacking;
        }
        private async void BtnAttach_clicked(object sender, EventArgs e)
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("Sin cámara", "No hay cámara disponible", "OK");
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

            missingLabel.IsVisible = false;

            if (Image1.Source == null)
            {
                Image1.Source = ImageSource.FromStream(() => file.GetStream());
                App.ImagesNames.Add(imgName);
                App.ImagesStreams.Add(imgStream);
                await AnimateImage(Image1);
            }
            else if (Image2.Source == null)
            {
                Image2.Source = ImageSource.FromStream(() => file.GetStream());
                App.ImagesNames.Add(imgName);
                App.ImagesStreams.Add(imgStream);
                await AnimateImage(Image2);
            }
            else if (Image3.Source == null)
            {
                Image3.Source = ImageSource.FromStream(() => file.GetStream());
                App.ImagesNames.Add(imgName);
                App.ImagesStreams.Add(imgStream);
                await AnimateImage(Image3);
            }
            else
            {
                await DisplayAlert("Campo lleno", "Ha alcanzado el numero máximo de imágenes por reporte", "Aceptar");
            }

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
        private async Task AnimateImage(Xamarin.Forms.Image image)
        {
            uint duration = 300;

            image.IsVisible = true;

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