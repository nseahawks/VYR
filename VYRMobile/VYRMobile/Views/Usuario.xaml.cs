﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views
{
    public partial class Usuario : ContentPage
    {
        public Usuario()
        {
            InitializeComponent();

            Logout.Clicked += Logout_clicked;
            Estadisticas.Clicked += Estadisticas_clicked;
            Equipamiento.Clicked += Equipamiento_clicked;
        }

        private void Equipamiento_clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Utensilios());
        }

        private void Estadisticas_clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Estadisticas());
        }

        private void Logout_clicked(object sender, EventArgs e)
        {
            Navigation.PushModalAsync(new Login());
        }
    }
}