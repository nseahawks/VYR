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

namespace VYRMobile.ViewModels
{
    public class ReportViewModel : BindableObject
    {
        //Crear Reporte
        public Report CReport { get; set; }
        public Command CreateReportCommand { get; }
        public Command LoadCommand { get; set; }
        public Command ReportDetailsCommand { get; set; }


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
        public string Title
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

        public Report.ReportStatuses Status
        {
            get => CReport.ReportStatus;
            set
            {
                if (CReport.ReportStatus == value)
                    return;
                CReport.ReportStatus = value;
                OnPropertyChanged(nameof(Status));
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
        private ReportsStore _store { get; }

     
        public bool isBusy = false;

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

        public ReportViewModel()
        {
            ImageInfo = new ObservableCollection<Models.Image>();
            Reports = new ObservableCollection<Models.Report>();

            LoadPosts();
            LoadData();

            CReport = new Report();
            _store = new ReportsStore();
            CreateReportCommand = new Command(async () => await CreateReport());
            LoadCommand = new Command(async () => await LoadData2());
            ReportDetailsCommand = new Command(async () => await LoadData2());
            _typeCollection = new ObservableCollection<string>(Enum.GetNames(typeof(Report.ReportTypes)));
            _statusCollection = new ObservableCollection<string>(Enum.GetNames(typeof(Report.ReportStatuses)));
        }

        private async Task CreateReport()
        {
            IsSuccess = await _store.AddReportAsync(CReport);
            await LoadData2();
            await App.Current.MainPage.Navigation.PopAsync();
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
        }

        private void LoadPosts()
        {
            var posts = MockImageService.Instance.GetCommunityPosts();
            Posts = new ObservableCollection<Models.Image>(posts);
            CurrentPost = Posts[0];
        }
    }
}
