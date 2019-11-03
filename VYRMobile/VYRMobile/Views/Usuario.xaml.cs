﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Usuario : ContentPage
    {
        public Usuario()
        {
            InitializeComponent();

            Logout.Clicked += Logout_clicked;
        }

        private void Logout_clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Login());
        }
    }
}