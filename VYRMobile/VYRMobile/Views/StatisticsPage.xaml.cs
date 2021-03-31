using System;
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
            Navigation.PushModalAsync(new NavigationPage(new Page1(User))
            {
                BarBackgroundColor = (Color)Application.Current.Resources["PrimaryColor"],
                BarTextColor = (Color)Application.Current.Resources["SecondaryColor"],
            });
        }

        protected override void OnDisappearing()
        {
            animation.IsVisible = false;
            animation.IsPlaying = false;
        }

        protected override void OnAppearing()
        {
            if(App.ApplicationUserRole == "Admin")
            {
                userComboBox.IsVisible = false;
                userComboBox.IsEnabled = false;
                btnCalificar.IsVisible = false;
                btnCalificar.IsEnabled = false;
            }
        }

        private void SfLinearProgressBar_ProgressCompleted(object sender, Syncfusion.XForms.ProgressBar.ProgressValueEventArgs e)
        {
            animation.IsVisible = true;
            animation.IsPlaying = true;
        }

        private void userComboBox_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            var selection = e.Value as ApplicationUser;

            if (selection == null)
            {
                btnCalificar.IsEnabled = false;
            }
            else
            {
                btnCalificar.IsEnabled = true;
                User = selection.FullName;
                App.ReviewedUserId = selection.Id;
            }
        }

        private void UserFilter()
        {
            if(App.ApplicationUserRole == "Supervisor")
            {
                userComboBox.IsVisible = true;
                userComboBox.IsEnabled = true;
                btnCalificar.IsVisible = true;
            }
        }
    }
}