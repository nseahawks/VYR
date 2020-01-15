using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VYRMobile.Models
{
    public class Image : ObservableObject
    {
        private int ind;
        public int Ind 
        {
            get => ind;
            set => SetProperty(ref ind, value);
        }
        private string source;
        public string Source 
        { 
            get => source;
            set => SetProperty(ref source, value);
        }
    }
}
