using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views
{
    public partial class Utensilios : ContentPage
    {
        public Utensilios()
        {
            InitializeComponent();
            BindingContext = new EquipoViewModel();
            btnConfirmar.Clicked += BtnConfirmar_Clicked;
        }

        private async void BtnConfirmar_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Confirmacion", "¿Acepta los términos y condiciones de uso?", "ACEPTAR", "CANCELAR");
            Application.Current.MainPage = new NavigationPage(new Loading());
        }
    }
}