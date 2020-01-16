using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using VYRMobile.Services;
using Plugin.Messaging;

namespace VYRMobile.ViewModels
{
    public class CallViewModel : BindableObject
    {
        public ICommand ItemSelectedCommand => new Command<string>(ItemSelected);
        public CallViewModel()
        {

        }
        private void ItemSelected(string parameter)
        {
            var phoneCallTask = CrossMessaging.Current.PhoneDialer;
            if (phoneCallTask.CanMakePhoneCall)
            {
                phoneCallTask.MakePhoneCall(parameter, "Francisco Rojas");
            }
        }
    }
}
