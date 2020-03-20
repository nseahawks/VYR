using Firebase.Storage;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using VYRMobile.Helper;
using VYRMobile.ViewModels;
using Plugin.FilePicker;

namespace VYRMobile.Views
{
    public partial class Test : ContentPage
    {
        bool IsUrgent = false;
        public Test()
        {
            InitializeComponent();
            BindingContext = new PuntoViewModel();
            //alarmMode();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
        private async void alarmMode()
        {
            await Task.Delay(2500);
            IsUrgent = true;
            while(IsUrgent)
            {
                await animation.FadeTo(0.5, 500, Easing.Linear);
                await animation.FadeTo(1, 500, Easing.Linear);
            }
        }

    }
}