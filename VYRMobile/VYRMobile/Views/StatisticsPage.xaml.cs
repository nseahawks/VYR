using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatisticsPage : ContentPage
    {
        public StatisticsPage()
        {
            InitializeComponent();

            BindingContext = new StatsViewModel();
        }
        /*protected override void OnAppearing()
        {
        }*/
        protected override void OnDisappearing()
        {
            animation.IsVisible = false;
            animation.IsPlaying = false;
        }

        private void SfLinearProgressBar_ProgressCompleted(object sender, Syncfusion.XForms.ProgressBar.ProgressValueEventArgs e)
        {
            animation.IsVisible = true;
            animation.IsPlaying = true;
        }
    }
}