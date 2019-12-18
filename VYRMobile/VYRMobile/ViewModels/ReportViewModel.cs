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

namespace VYRMobile.ViewModels
{
    class ReportViewModel : BindableObject
    {
        //Crear Reporte
        public Report CReport { get; set; }
        public Command CreateReportCommand { get; }
        //Need to be implemented
        //public Command AttachCommand { get; }

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
                //password = value;
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
            get => CReport.Status;
            set
            {
                if (CReport.Status == value)
                    return;
                CReport.Status = value;
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
        private async Task CreateReport()
        {
            IsSuccess = await _store.AddReportAsync(CReport);
        }

        //Lista de Reportes
        private ObservableCollection<Models.Report> _reports;


        public ReportViewModel()
        {
            Reports = new ObservableCollection<Models.Report>();

            LoadData();
            CReport = new Report();
            _store = new ReportsStore();
            CreateReportCommand = new Command(async () => await CreateReport());
            //AttachCommand = new Command(Attach);
            _typeCollection = new ObservableCollection<string>(Enum.GetNames(typeof(Report.ReportTypes)));
            _statusCollection = new ObservableCollection<string>(Enum.GetNames(typeof(Report.ReportStatuses)));
        }
        public ObservableCollection<Models.Report> Reports
        {
            get { return _reports; }
            set
            {
                _reports = value;
                OnPropertyChanged();
            }
        }

        
       
        private async void LoadData()
        {
            var reports = await ReportsStore.Instance.GetReportsAsync();

            Reports.Clear();
            foreach (var report in reports)
            {
                Reports.Add(report);
            }
        }

        /*private void Types()
        {
            foreach (string reportType in Enum.GetNames(typeof(Report.ReportTypes)))
            {
                ReportTypes.Add(reportType);
            }
        }*/

        
        /*private void ItemSelected(string parameter)
        {
            var reports = ReportService.Instance.GetReports();

            Reports.Clear();


        }*/
    }
}
