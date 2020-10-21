using System;
using VYRMobile.Models;
using VYRMobile.ViewModels;
using Xamarin.Forms;

namespace VYRMobile.Views
{
    public partial class ReportsPage : ContentPage
    {
        public ReportsPage()
        {
            InitializeComponent();

            
            btnReporte.Clicked += btnReporte_Clicked;
            reportsView.ItemTapped += ReportsView_ItemTapped;

            BindingContext = ReportViewModel.Instance;
        }
        private async void ReportsView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var sel = e.Item as Report;

            await Navigation.PushModalAsync(new NavigationPage (new ReportDetailsPage(sel.Title, sel.Description, sel.TypeIcon, sel.ReportType, sel.ReportStatus, sel.Created, sel.StatusColor, sel.Img))
            {
                BarBackgroundColor = (Color)Application.Current.Resources["PrimaryColor"],
                BarTextColor = (Color)Application.Current.Resources["SecondaryColor"]
            });
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
        }
        private async void btnReporte_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new CreateReportPage())
            {
                BarBackgroundColor = (Color)Application.Current.Resources["PrimaryColor"],
                BarTextColor = (Color)Application.Current.Resources["SecondaryColor"]
            });
        }
    }
}