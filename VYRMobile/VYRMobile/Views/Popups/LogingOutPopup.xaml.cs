﻿using Plugin.CloudFirestore;
using Plugin.DeviceInfo;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LogingOutPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public LogingOutPopup()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            LogingOut();
        }
        protected override bool OnBackgroundClicked()
        {
            return false;
        }
        private async void LogingOut()
        {
            await Task.Delay(50);

            string _userId = App.ApplicationUserId;

            await CrossCloudFirestore.Current.Instance
                                      .GetCollection("usersApp")
                                      .GetDocument(_userId)
                                      .UpdateDataAsync(new { LoggedIn = false });

            Navigation.InsertPageBefore(new Login(), Navigation.NavigationStack[0]);
            await Navigation.PopToRootAsync();
            await Navigation.PopPopupAsync();
        }
    }
}