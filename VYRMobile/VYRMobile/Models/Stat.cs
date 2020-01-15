using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace VYRMobile.Models
{
    public class Stat : ObservableObject
    {
        public string Month { get; set; }
        public double Target { get; set; }

        public Stat(string xValue, double yValue)
        {
            Month = xValue;
            Target = yValue;
        }
    }
}
