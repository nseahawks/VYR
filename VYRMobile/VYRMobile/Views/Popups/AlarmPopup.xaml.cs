using Plugin.CloudFirestore;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Threading.Tasks;
using VYRMobile.ViewModels;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlarmPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        bool IsShaking = true;
        public AlarmPopup(string locationName, GeoPoint position, string locationType)
        {
            InitializeComponent();
            BindingContext = new PuntoViewModel();

            locationNameLbl.Text = locationName;

            App.Alarm.LocationName = locationName;
            App.Alarm.Location = position;

            shakeImage();

            /*App.Alarm.LocationName = locationName;
            App.Alarm.Type = locationType;*/
        }
        public AlarmPopup()
        {
            InitializeComponent();
            BindingContext = new PuntoViewModel();

            shakeImage();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        protected override bool OnBackgroundClicked()
        {
            return false;
        }
        private async void shakeImage()
        {
            while (IsShaking) 
            {
                alarmOne.IsVisible = false;
                alarmTwo.IsVisible = true;
                await Task.Delay(60);
                alarmTwo.IsVisible = false;
                alarmOne.IsVisible = true;
                await Task.Delay(60);
            }
        }
    }
}