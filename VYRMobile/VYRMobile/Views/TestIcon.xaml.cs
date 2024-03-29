﻿using VYRMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestIcon : ContentPage
    {
        private static TestIcon _instance;
        public static TestIcon Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new TestIcon("");

                return _instance;
            }
        }
        public TestIcon(string workerId)
        {
            InitializeComponent();

            BindingContext = new EquipmentViewModel(workerId);
        }
    }
}