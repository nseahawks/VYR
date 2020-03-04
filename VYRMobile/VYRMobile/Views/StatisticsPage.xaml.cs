using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.Models;
using VYRMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatisticsPage : ContentPage
    {
        string User;
        public StatisticsPage()
        {
            InitializeComponent();

            BindingContext = new StatsViewModel();
            btnCalificar.Clicked += BtnCalificar_Clicked;
            UserFilter();
        }

        private void BtnCalificar_Clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new NavigationPage(new EvaluationPage(User)));
        }

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

        private void userComboBox_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            var selection = e.Value as ApplicationUser;
            User = selection.Name;
        }

        private void UserFilter()
        {
            if(App.ApplicationUserRole == "User")
            {
                userComboBox.IsVisible = false;
                btnCalificar.IsVisible = false;
            }
        }
    }
}