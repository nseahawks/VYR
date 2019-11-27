using MvvmHelpers;
using System.Threading.Tasks;
using VYRMobile.Models;
using VYRMobile.Data;
using Xamarin.Forms;

namespace VYRMobile.ViewModels
{
    class CreateReportViewModel : BaseViewModel
    {
        public Report Report { get; set; }
        public Command CreateReportCommand { get; }
        //Need to be implemented
        //public Command AttachCommand { get; }

        private ReportsStore _store { get; }

        public CreateReportViewModel() 
        {
            Report =  new Report();
            _store = new ReportsStore();
            CreateReportCommand = new Command(async () => await CreateReport());
            //AttachCommand = new Command(Attach);


        }
        public bool isBusy = false;

        //IMPLEMENT FROM HERE LATER
        //private async void Attach()
        //{
        //    await CrossMedia.Current.Initialize();

        //    if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
        //    {
        //        await Application.Current.MainPage.DisplayAlert("No Camera", "No camera available", "OK");
        //        return;
        //    }

        //    var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
        //    {
        //        SaveToAlbum = true,
        //        Name = "seahawks.png"
        //    });

        //    if (file == null)
        //        return;

        //    Image1.Source = ImageSource.FromStream(() => file.GetStream());
        //}

        private async Task CreateReport() 
        {
            await _store.AddReportAsync(Report);
        }

    }
}
