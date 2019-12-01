using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace VYRMobile.Models
{
    public class Event : ObservableObject
    {
        string eventId;
        public string EventId
        {
            get => eventId;
            set => SetProperty(ref eventId, value);
        }

        string eventIcon;
        public string EventIcon
        {
            get => eventIcon;
            set => SetProperty(ref eventIcon, value);
        }

        string eventName;
        public string EventName
        {
            get => eventName;
            set => SetProperty(ref eventName, value);
        }

        string owner;
        public string Owner
        {
            get => owner;
            set => SetProperty(ref owner, value);
        }

        string time;
        public string Time
        {
            get => time;
            set => SetProperty(ref time, value);
        }
    }
}
