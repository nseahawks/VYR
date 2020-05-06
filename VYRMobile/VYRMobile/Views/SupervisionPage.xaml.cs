using Syncfusion.SfPicker.XForms;
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
    public partial class SupervisionPage : ContentPage
    {
        ApplicationUser user;
        public SupervisionPage()
        {
            InitializeComponent();

            BindingContext = new SupervisionViewModel();
        }
        private void OnValidate(object sender, EventArgs e)
        {
            var viewCellSelected = sender as MenuItem;
            var user = viewCellSelected?.BindingContext as ApplicationUser;

            Navigation.PushAsync(new NavigationPage(new Test())
            {
                BarBackgroundColor = (Color)Application.Current.Resources["PrimaryColor"],
                BarTextColor = (Color)Application.Current.Resources["SecondaryColor"]
            });
        }

        private void OnReplace(object sender, EventArgs e)
        {
            var viewCellSelected = sender as MenuItem;
            var user = viewCellSelected?.BindingContext as ApplicationUser;

            pickerView.IsEnabled = true;
            pickerView.IsVisible = true;
        }

        private void Picker_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            var appUser = e.NewValue as ApplicationUser;

            SupervisionViewModel supervisionViewModel = new SupervisionViewModel(user, appUser);

            workersView.ItemsSource = supervisionViewModel.Users;

            picker.IsOpen = false;
        }

        private void Picker_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            picker.IsOpen = false;
        }
    }
}