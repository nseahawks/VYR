using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Reportes : ContentPage
    {
        public Reportes()
        {
            InitializeComponent();
            btnReporte.Clicked += btnReporte_Clicked;
        }
        private void btnReporte_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CrearReporte());
        }
    }
}