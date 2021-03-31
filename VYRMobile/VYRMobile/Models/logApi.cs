using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace VYRMobile.Models
{
    public class logApi : ObservableObject
    {
        string Id;
        public string id
        {
            get => Id;
            set => SetProperty(ref Id, value);
        }
        string LogStatus;
        public string logStatus
        {
            get => LogStatus;
            set => SetProperty(ref LogStatus, value);
        }
        double Latitude;
        public double latitude
        {
            get => Latitude;
            set => SetProperty(ref Latitude, value);
        }
        double Longitude;
        public double longitude
        {
            get => Longitude;
            set => SetProperty(ref Longitude, value);
        }
        string LogComment;
        public string logComment
        {
            get => LogComment;
            set => SetProperty(ref LogComment, value);
        }
        string User;
        public string user
        {
            get => User;
            set => SetProperty(ref User, value);
        }
        string UserChecked;
        public string userChecked
        {
            get => UserChecked;
            set => SetProperty(ref UserChecked, value);
        }
    }
}
