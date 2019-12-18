using System;
using System.Collections.Generic;
using System.Text;
using MvvmHelpers;

namespace VYRMobile.Models
{
    public class Antena : ObservableObject
    {
        string antenaId;
        public string AntenaId
        {
            get => antenaId;
            set => SetProperty(ref antenaId, value);
        }

        string antenaName;
        public string AntenaName
        {
            get => antenaName;
            set => SetProperty(ref antenaName, value);
        }

        string cellId;
        public string CellId
        {
            get => cellId;
            set => SetProperty(ref cellId, value);
        }

        string antenaAddress;
        public string AntenaAddress
        {
            get => antenaAddress;
            set => SetProperty(ref antenaAddress, value);
        }

        double antenaLatitude;
        public double AntenaLatitude
        {
            get => antenaLatitude;
            set => SetProperty(ref antenaLatitude, value);
        }

        double antenaLongitude;
        public double AntenaLongitude
        {
            get => antenaLongitude;
            set => SetProperty(ref antenaLongitude, value);
        }

        string userId;
        public string UserId
        {
            get => userId;
            set => SetProperty(ref userId, value);
        }
    }
}
