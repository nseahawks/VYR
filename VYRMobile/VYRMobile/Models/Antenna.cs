/*using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace VYRMobile.Models
{
    public class Antenna : ObservableObject
    {
        private string locationName, cellId, address;
        private Guid id;
        private double latitude, longitude;
        private object userId, getAlarm;
        private object[] getRoutes;
        private bool isChecked;

        public string LocationName
        {
            get => locationName;
            set
            {
                SetProperty(ref locationName, value);
            }
        }
        public string CellId
        {
            get => cellId;
            set
            {
                SetProperty(ref cellId, value);
            }
        }
        public string Address
        {
            get => address;
            set
            {
                SetProperty(ref address, value);
            }
        }
        public Guid Id
        {
            get => id;
            set
            {
                SetProperty(ref id, value);
            }
        }
        public double Latitude
        {
            get => latitude;
            set
            {
                SetProperty(ref latitude, value);
            }
        }
        public double Longitude
        {
            get => longitude;
            set
            {
                SetProperty(ref longitude, value);
            }
        }
        public object UserId
        {
            get => userId;
            set
            {
                SetProperty(ref userId, value);
            }
        }
        public object[] GetRoutes
        {
            get => getRoutes;
            set
            {
                SetProperty(ref getRoutes, value);
            }
        }
        public object GetAlarm
        {
            get => getAlarm;
            set
            {
                SetProperty(ref getAlarm, value);
            }
        }
        public bool IsChecked
        {
            get => isChecked;
            set
            {
                isChecked = value;
                OnPropertyChanged("PointChecked");
            }
        }
    }
}*/
