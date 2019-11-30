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

        private ReportsStore _store { get; }

     
        public bool isBusy = false;

        private async Task CreateReport()
        {
            await _store.AddReportAsync(CReport);
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
        public ICommand ItemSelectedCommand => new Command<string>(ItemSelected);

        private void LoadData()
        {
            var reports = ReportService.Instance.GetReports();

            Reports.Clear();
            foreach (var report in reports)
            {
                Reports.Add(report);
            }
        }
        private void ItemSelected(string parameter)
        {
            var reports = ReportService.Instance.GetReports();

            Reports.Clear();


        }
    }
}
