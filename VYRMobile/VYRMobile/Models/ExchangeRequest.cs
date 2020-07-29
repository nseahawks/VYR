﻿using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace VYRMobile.Models
{
    public class ExchangeRequest : ObservableObject
    {
        private bool exchange;
        public bool Exchange
        {
            get => exchange;
            set => SetProperty(ref exchange, value);
        }
    }
}
