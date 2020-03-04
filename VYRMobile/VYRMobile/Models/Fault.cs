using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace VYRMobile.Models
{
    public class Fault : ObservableObject
    {
        string faultName;
        public string FaultName
        {
            get => faultName;
            set
            {
                SetProperty(ref faultName, value);
            }
        }
        public Fault(string fault)
        {
            FaultName = fault;
        }
    }
}
