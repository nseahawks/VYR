﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CaptionPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public CaptionPopup()
        {
            InitializeComponent();
        }
    }
}