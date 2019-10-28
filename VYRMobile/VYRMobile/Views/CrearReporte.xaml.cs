using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;

namespace VYRMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CrearReporte : ContentPage
    {
        public CrearReporte()
        {
            InitializeComponent();
            btnAtras.Clicked += btnAtras_clicked;
            btnAttach.Clicked += btnAttach_clicked;
            BindingContext = new CrearReporteModel();
        }

        private async void btnAttach_clicked(object sender, EventArgs e)
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

        private void btnAtras_clicked(object sender, EventArgs e)
        {
            ((NavigationPage)this.Parent).PushAsync(new Reportes());
        }
        
    }
}