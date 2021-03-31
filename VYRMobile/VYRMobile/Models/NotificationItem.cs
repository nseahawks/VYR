using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace VYRMobile.Models
{
    public class NotificationItem : ObservableObject
    {
        string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        string message;
        public string Message
        {
            get => message;
            set => SetProperty(ref message, value);
        }
        bool isNew;
        public bool IsNew
        {
            get => isNew;
            set => SetProperty(ref isNew, value);
        }
        string owner;
        public string Owner
        {
            get => owner;
            set => SetProperty(ref owner, value);
        }
        DateTime date;
        public DateTime Date
        {
            get => date;
            set => SetProperty(ref date, value);
        }
        string icon;
        public string Icon
        {
            get => icon;
            set => SetProperty(ref icon, value);
        }
    }
}
