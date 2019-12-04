using MvvmHelpers;
using System.Threading.Tasks;
using VYRMobile.Models;
using VYRMobile.Data;
using Xamarin.Forms;

namespace VYRMobile.ViewModels
{
    class CreateReportViewModel : BaseViewModel
    {
        Report CReport { get; set; }
        public Command CreateReportCommand { get; }
        //Need to be implemented
        //public Command AttachCommand { get; }

        private ReportsStore _store { get; }

        public new string Title
        {
            get => CReport.Title;
            set
            {
                if (CReport.Title == value)
                    return;
                CReport.Title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public string Description
        {
            get => CReport.Description; 
            set
            {
                if (CReport.Description == value)
                    return;
                CReport.Description = value;
                //password = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        bool isSuccess;
        public bool IsSuccess
        {
            get
            {
                return isSuccess;
            }
            set
            {
                if (isSuccess != value)
                {
                    isSuccess = value;
                    OnPropertyChanged(nameof(IsSuccess));
                }
            }
        }
        public CreateReportViewModel() 
        {
            CReport =  new Report();
            _store = new ReportsStore();
            CreateReportCommand = new Command(async () => await CreateReport());
            //AttachCommand = new Command(Attach);


        }
        
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
            IsSuccess = await _store.AddReportAsync(CReport);
        }

    }
}
