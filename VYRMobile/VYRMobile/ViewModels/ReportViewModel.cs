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
using System.ComponentModel;

namespace VYRMobile.ViewModels
{
    class ReportViewModel : BindableObject
    {
        //Crear Reporte
        public Report CReport { get; set; }
        public Command CreateReportCommand { get; }

        //Need to be implemented
        //public Command AttachCommand { get; }

        private ReportsStore _store { get; }


     
        public bool isBusy = false;

        private async Task CreateReport()
        {
            await _store.AddReportAsync(CReport);
        }

        //Lista de Reportes
        private ObservableCollection<Models.Report> _reports;

        private ObservableCollection<Models.Report.ReportTypes> _reportTypes;

        private ObservableCollection<Models.Report.ReportStatuses> _statuses;

        public ReportViewModel()
        {
            Reports = new ObservableCollection<Models.Report>();

            ReportTypes = new ObservableCollection<Models.Report.ReportTypes>();

            Statuses = new ObservableCollection<Models.Report.ReportStatuses>();

            LoadData();
            Types();
            CReport = new Report();
            _store = new ReportsStore();
            CreateReportCommand = new Command(async () => await CreateReport());
            //AttachCommand = new Command(Attach);
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

        public ObservableCollection<Models.Report.ReportTypes> ReportTypes
        {
            get { return _reportTypes; }
            set
            {
                _reportTypes = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Models.Report.ReportStatuses> Statuses
        {
            get { return _statuses; }
            set
            {
                _statuses = value;
                OnPropertyChanged();
            }
        }
        //public ICommand ItemSelectedCommand => new Command<string>(ItemSelected);

        private async void LoadData()
        {
            var reports = await ReportsStore.Instance.GetReportsAsync();

            Reports.Clear();
            foreach (var report in reports)
            {
                Reports.Add(report);
            }
        }

        private void Types()
        {
            var statuses = Report.Instance.GetStatuses();

            Statuses.Clear();
            foreach (var status in statuses)
            {
                Statuses.Add(status);
            }
        }


        /*private void ItemSelected(string parameter)
        {
            var reports = ReportService.Instance.GetReports();

            Reports.Clear();


        }*/
    }
}
