using System;
using System.Collections.Generic;
using System.Text;
using MvvmHelpers;

namespace VYRMobile.Models
{
    public class Punto : ObservableObject
    {
        string pointId;
        public string PointId
        {
            get => pointId;
            set => SetProperty(ref pointId, value);
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
            set => SetProperty(ref pointChecked, value);
        }
    }
}
