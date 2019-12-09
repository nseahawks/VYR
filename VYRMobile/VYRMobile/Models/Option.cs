using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace VYRMobile.Models
{
    public class Option : ObservableObject
    {
        string optionIcon;
        public string OptionIcon
        {
            get => optionIcon;
            set => SetProperty(ref optionIcon, value);
        }


        string optionName;
        public string OptionName
        {
            get => optionName;
            set => SetProperty(ref optionName, value);
        }
        
    }
}
