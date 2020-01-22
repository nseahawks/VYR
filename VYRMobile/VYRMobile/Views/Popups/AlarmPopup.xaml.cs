using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlarmPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        bool IsShaking = true;
        public AlarmPopup()
        {
            InitializeComponent();
            BindingContext = new PuntoViewModel();

            shakeImage();
            //OK.Clicked += OK_Clicked;
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
        private async void OK_Clicked(object sender, EventArgs e) 
        {
            await Navigation.PopPopupAsync();
            //puntoVM.StartCommand.Execute(null);
        }
    }
}