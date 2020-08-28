using System.Collections.Generic;
using System.Linq;
using VYRMobile.ViewModels;
using Xamarin.Forms;
using VYRMobile.Models;

namespace VYRMobile
{
    public partial class HistoryPage : ContentPage
    {
        List<Record> Records = new List<Record>();
        public HistoryPage()
        {
            InitializeComponent();
            BindingContext = new EventViewModel();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            LoadRecordList();
        }
        private void LoadRecordList()
        {
            if (App.Records != null)
            {
                Records.Clear();
                foreach(var record in App.Records)
                {
                    if (record.UserId == App.ApplicationUserId)
                    {
                        Records.Add(record);
                    }
                }
                Records = new List<Record>(Records.OrderByDescending(records => records.Date).ToList());
                recordList.ItemsSource = Records;
            }
        }
    }
}