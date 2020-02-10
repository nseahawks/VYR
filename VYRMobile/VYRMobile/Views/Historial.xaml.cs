using Plugin.Geolocator;
using Plugin.Messaging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VYRMobile.ViewModels;
using VYRMobile.Views;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;
using ZXing.Net.Mobile.Forms;

using System.Windows.Input;
using System.IO;
using VYRMobile.Models;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Plugin.Settings;
using PCLStorage;
using VYRMobile.Services;
using VYRMobile.Helper;

namespace VYRMobile
{
    public partial class Historial : ContentPage
    {
        //RecordHelper _record = new RecordHelper();
        List<Record> Records = new List<Record>();
        public Historial()
        {
            InitializeComponent();
            /*BindingContext = new CallViewModel();
            BindingContext = new CronoViewModel();
            BindingContext = new QRViewModel();*/

            BindingContext = new EventViewModel();

            //AlertMain();
            /*btnStart.Clicked += BtnStart_Clicked;
            btnStop.Clicked += BtnStop_Clicked;*/
            /*QR.Clicked += QR_Clicked;
            CallFrancisco.Clicked += CallFrancisco_clicked;
            alert.Clicked += alert_clicked;*/

            /*Menu.ItemTapped += async (sender, e) =>
            {
                var evnt = (SelectedItemChangedEventArgs)e;
                Notifier.Text = (string)evnt.SelectedItem;
                await Task.Delay(2000);
                Notifier.Text = "";

            };*/
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