using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;

namespace VYRMobile.ViewModels
{
    public class TabViewModel : BaseViewModel
    {
        string count = "*";
        public string Count
        {
            get { return count; }
            set { SetProperty(ref count, value); }
        }
        Color color = Color.Orange;
        public Color Color
        {
            get { return color; }
            set { SetProperty(ref color, value); }
        }

    }
}
