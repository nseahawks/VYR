using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using MvvmHelpers;

namespace VYRMobile.Models
{
    public class Punto : ObservableObject
    {
        /*public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }*/

        string pointId;
        public string PointId
        {
            get => pointId;
            set 
            {
                SetProperty(ref pointId, value);
                //PropertyChanged(this, new PropertyChangedEventArgs("PointId"));
            }
        }

        string pointName;
        public string PointName
        {
            get => pointName;
            set => SetProperty(ref pointName, value);
        }

        bool pointChecked;
        public bool PointChecked
        {
            get => pointChecked;
            set
            {
                pointChecked = value;
                OnPropertyChanged("PointChecked");
                /*PropertyChanged(this, new PropertyChangedEventArgs("PointChecked"));
                OnPropertyChanged("PointChecked");*/
            }
        }
    }
}
