using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace VYRMobile.Models
{
    public class Record : ObservableObject
    {
        string userId;
        public string UserId
        {
            get => userId;
            set => SetProperty(ref userId, value);
        }
        string type;
        public string Type
        {
            get => type;
            set => SetProperty(ref type, value);
        }
        RecordTypes recordType;
        public RecordTypes RecordType
        {
            get => recordType;
            set => SetProperty(ref recordType, value);
        }
        public enum RecordTypes
        {
            LogIn,
            AlarmAccepted,
            AlarmAssisted,
            RouteStarted,
            AntennaCovered,
            Call
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
