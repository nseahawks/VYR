using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Reportes : ContentPage
    {
        HttpClient _client;
        public Reportes()
        {
            InitializeComponent();
            btnReporte.Clicked += btnReporte_Clicked;
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
            Navigation.PushAsync(new CreateReportPage());
        }
    }
}