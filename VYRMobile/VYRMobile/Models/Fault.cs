using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace VYRMobile.Models
{
    public class Fault : ObservableObject
    {
        string name;
        public string Name
        {
            get => name;
            set
            {
                SetProperty(ref name, value);
            }
        }
        int severity;
        public int Severity
        {
            get => severity;
            set
            {
                SetProperty(ref severity, value);
            }
        }
        public Fault(string fault)
        {
            Name = fault;
        }
    }
}
