using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using VYRMobile.Services;

namespace VYRMobile.ViewModels
{
    class ReportViewModel : BindableObject
    {
        private ObservableCollection<Models.Report> _reports;

        public ReportViewModel()
        {
            Reports = new ObservableCollection<Models.Report>();

            LoadData();
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
