using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.Models;
using VYRMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views
{
    public partial class Reportes : ContentPage
    {
        //HttpClient _client;
        public Reportes()
        {
            InitializeComponent();

            btnReporte.Clicked += btnReporte_Clicked;
            reportsView.ItemTapped += ReportsView_ItemTapped;
            closeReport.Clicked += CloseReport_Clicked;

            BindingContext = new ReportViewModel();
        }
        private void CloseReport_Clicked(object sender, EventArgs e)
        {
            reportDetails.IsVisible = false;
        }
        private async void ReportsView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var sel = e.Item as Report;

            /*iconImage.Source = sel.TypeIcon;
            titleLabel.Text = sel.Title;
            typeLabel.Text = sel.ReportType.ToString();
            descriptionLabel.Text = sel.Description;
            statusLabel.Text = sel.ReportStatus.ToString();
            colorBoxView.BackgroundColor = sel.StatusColor;
            dateLabel.Text = sel.Created.ToString();*/

            await Navigation.PushModalAsync(new NavigationPage (new ReportDetailsPage(sel.Title, sel.Description, sel.TypeIcon, sel.ReportType, sel.ReportStatus, sel.Created, sel.StatusColor, sel.Img)));
            /*ReportViewModel rvm = new ReportViewModel(sel.Title, sel.Description, sel.TypeIcon, sel.ReportType, sel.ReportStatus, sel.Created, sel.StatusColor);

            uint duration = 300;

            reportDetails.IsVisible = true;

            await reportDetails.FadeTo(0, 0);

            var animation = new Animation();

            animation.WithConcurrent((f) => reportDetails.Opacity = f, 0, 1, Easing.CubicIn);

            animation.WithConcurrent(
              (f) => reportDetails.TranslationY = f,
              reportDetails.TranslationY + 100, 0,
              Easing.CubicIn, 0, 1);

            reportDetails.Animate("FadeIn", animation, 16, Convert.ToUInt32(duration));*/

            
            //await reportDetails.FadeTo(100, 100, Easing.Linear);

        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            //HttpClient client = _api.VRAPI();
            //_client = new HttpClient();
            //string url = "https://vyrapi.azurewebsites.net/api/v1/reports";
            //string url = "https://169.254.80.80:5001/api/v1/reports";

            //try
            //{
                
                
            //    var response = await _client.GetAsync(url);
            //    if (response.IsSuccessStatusCode)
            //    {
            //        string json = await response.Content.ReadAsStringAsync();
            //        //ReportViewModel reports = ReportViewModel.FromJson(json);
            //        //reportList.ItemsSource = reports.Reports;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine(@"\tERROR {0}", ex.Message);
            //}


            //string json = await response.Content.ReadAsStringAsync();
            //ReportViewModel reports = ReportViewModel.FromJson(json);

            //client.Dispose();
        }
        private void btnReporte_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new NavigationPage(new CreateReportPage()));
        }
    }
}