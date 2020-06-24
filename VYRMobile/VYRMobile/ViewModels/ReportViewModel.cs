using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using VYRMobile.Services;
using VYRMobile.Models;
using VYRMobile.Data;
using System.Threading.Tasks;
using VYRMobile.Views;
using static Android.Content.ClipData;
using System.Collections;
using System.Reflection;
using System.IO;
using Newtonsoft.Json;
using System.Collections.Generic;
using Rg.Plugins.Popup.Extensions;
using VYRMobile.Views.Popups;

namespace VYRMobile.ViewModels
{
    public class ReportViewModel : BindableObject
    {
        //Crear Reporte
        public bool isBusy = false;
        private ReportsStore _store { get; set; }
        public Report CReport { get; set; }
        public EvaluationReport EReport { get; set; }
        public Command CreateReportCommand { get; }
        public Command CreateEvaluationReportCommand { get; }
        public Command LoadCommand { get; set; }
        public Command ReportDetailsCommand { get; set; }

        private static ReportViewModel _instance;
        public static ReportViewModel Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ReportViewModel();

                return _instance;
            }
        }
        private ObservableCollection<Models.Image> _posts;
        public ObservableCollection<Models.Image> Posts
        {
            get { return _posts; }
            set
            {
                _posts = value;
                OnPropertyChanged();
            }
        }

        private Models.Image _currentPost;
        public Models.Image CurrentPost
        {
            get { return _currentPost; }
            set
            {
                _currentPost = value;
                OnPropertyChanged();
            }
        }
        public Antena Location
        {
            get => CReport.Location;
            set
            {
                if (CReport.Location == value)
                    return;
                CReport.Location = value;
                OnPropertyChanged(nameof(Location));
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
                OnPropertyChanged(nameof(Description));
            }
        }

        public Report.ReportTypes ReportType
        {
            get => CReport.ReportType;
            set
            {
                if (CReport.ReportType == value)
                    return;
                CReport.ReportType = value;
                OnPropertyChanged(nameof(ReportType));
            }
        }

        public Report.ReportStatuses ReportStatus
        {
            get => CReport.ReportStatus;
            set
            {
                if (CReport.ReportStatus == value)
                    return;
                CReport.ReportStatus = value;
                OnPropertyChanged(nameof(ReportStatus));
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

        private bool _isBusy;
        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }
        private bool _isEmpty;
        public bool IsEmpty
        {
            get { return _isEmpty; }
            set
            {
                _isEmpty = value;
                OnPropertyChanged(nameof(IsEmpty));
            }
        }
        private string name;
        public string ImageName
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged(nameof(ImageName));
            }
        }
        private Stream stream;
        public Stream ImageStream
        {
            get { return stream; }
            set
            {
                stream = value;
                OnPropertyChanged(nameof(ImageStream));
            }
        }

        private ObservableCollection<string> _typeCollection;
        public ObservableCollection<string> TypeCollection
        {
            get 
            { 
                return _typeCollection; 
            }
            set 
            { 
                _typeCollection = value; 
            }
        }

        private ObservableCollection<string> _statusCollection;
        public ObservableCollection<string> StatusCollection
        {
            get
            {
                return _statusCollection;
            }
            set
            {
                _statusCollection = value;
            }
        }

        //Lista de Reportes
        private ObservableCollection<Models.Report> _reports;
        public ObservableCollection<Models.Report> Reports
        {
            get { return _reports; }
            set
            {
                _reports = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Models.Image> imageInfo;
        public ObservableCollection<Models.Image> ImageInfo
        {
            get { return imageInfo; }
            set { imageInfo = value; }
        }
        private ObservableCollection<Models.Image> _reportImages;
        public ObservableCollection<Models.Image> ReportImages
        {
            get { return _reportImages; }
            set
            {
                _reportImages = value;
                OnPropertyChanged(nameof(ReportImages));
            }
        }


        private ObservableCollection<Antena> _locations;
        public ObservableCollection<Antena> Locations
        {
            get { return _locations; }
            set
            {
                _locations = value;
                OnPropertyChanged();
            }
        }
        public ReportViewModel()
        {
            Locations = new ObservableCollection<Antena>();
            ImageInfo = new ObservableCollection<Models.Image>();
            Reports = new ObservableCollection<Models.Report>();

            //LoadPosts();
            LoadLocations();
            LoadData();

            CReport = new Report();
            EReport = new EvaluationReport();
            _store = new ReportsStore();
            CreateReportCommand = new Command(async () => await CreateReport());
            CreateEvaluationReportCommand = new Command(async () => await CreateEvaluationReport());
            LoadCommand = new Command(async () => await LoadData2());
            ReportDetailsCommand = new Command(async () => await LoadData2());
            _typeCollection = new ObservableCollection<string>(Enum.GetNames(typeof(Report.ReportTypes)));
            _statusCollection = new ObservableCollection<string>(Enum.GetNames(typeof(Report.ReportStatuses)));
        }
        public ReportViewModel(string Img)
        {
            ReportImages = new ObservableCollection<Models.Image>();

            SplitString(Img);
        }

        private async void LoadLocations()
        {
            var locations = await ReportsStore.Instance.GetAntenasAsync();

            Locations.Clear();
            foreach (var location in locations)
            {
                Locations.Add(location);
            }
        }
        private async Task CreateReport()
        {
            if (IsBusy)
                return;

            CreateReportPage crp = new CreateReportPage();

            IsBusy = true;

            crp.DisableCommand.Execute(null);

            IsSuccess = await _store.AddReportAsync(CReport);

            if (IsSuccess == false)
            {
                crp.EnableCommand.Execute(null);
            }
            else
            {
                await LoadData2();
                await App.Current.MainPage.Navigation.PopModalAsync();
            }

            IsBusy = false;
        }
        private async Task CreateEvaluationReport()
        {
            if (IsBusy)
                return;

            await App.Current.MainPage.Navigation.PushPopupAsync(new LoadingPopup());

            IsBusy = true;
            IsSuccess = await _store.AddEvaluationReportAsync(EReport, App.Calculations);
            IsBusy = false;

            await App.Current.MainPage.Navigation.PopPopupAsync();
            await App.Current.MainPage.Navigation.PopModalAsync();
        }
        private async void LoadData()
        {
            var reports = await ReportsStore.Instance.GetReportsAsync();

            Reports.Clear();
            foreach (var report in reports)
            {
                int type = report.ReportType.GetHashCode();
                int status = report.ReportStatus.GetHashCode();

                switch (type)
                {
                    default:
                        report.TypeIcon = "base.png";
                        break;
                    case 1:
                        report.TypeIcon = "alarma.png";
                        break;
                    case 2:
                        report.TypeIcon = "robo.png";
                        break;
                    case 3:
                        report.TypeIcon = "asistencia.png";
                        break;
                    case 4:
                        report.TypeIcon = "dano.png";
                        break;
                }

                switch (status)
                {
                    default:
                        report.StatusColor = Color.FromHex("#938A8A");
                        break;
                    case 1:
                        report.StatusColor = Color.FromHex("#13FF8F");
                        break;
                    case 2:
                        report.StatusColor = Color.FromHex("#DD0808");
                        break;
                    case 3:
                        report.StatusColor = Color.FromHex("#FF9D13");
                        break;
                }
                Reports.Add(report);
            }
            Reports = new ObservableCollection<Report>(Reports.OrderByDescending(reports => reports.Created).ToList());

            if (Reports.Count == 0)
            {
                IsEmpty = true;
            }
            else
            {
                IsEmpty = false;
            }
        }
        private async Task LoadData2()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            var reports = await ReportsStore.Instance.GetReportsAsync();
            
            Reports.Clear();
            foreach (var report in reports)
            {
                int type = report.ReportType.GetHashCode();
                switch (type)
                {
                    default:
                        report.TypeIcon = "base.png";
                        break;
                    case 1:
                        report.TypeIcon = "alarma.png";
                        break;
                    case 2:
                        report.TypeIcon = "robo.png";
                        break;
                    case 3:
                        report.TypeIcon = "asistencia.png";
                        break;
                    case 4:
                        report.TypeIcon = "dano.png";
                        break;
                }
                Reports.Add(report);
            }

            IsBusy = false;
            Reports = new ObservableCollection<Report>(Reports.OrderByDescending(reports => reports.Created).ToList());

            if (Reports.Count == 0)
            {
                IsEmpty = true;
            }
            else
            {
                IsEmpty = false;
            }
        }
        private void SplitString(string Img)
        {
            string[] images = Img.Split(' ');

            foreach (var image in images)
            {
                ReportImages.Add(new Models.Image { Source = image });
            }
        }
    }
}
