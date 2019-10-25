using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CrearReporte : ContentPage
    {
        public CrearReporte()
        {
            InitializeComponent();
            btnAtras.Clicked += btnAtras_clicked;
            BindingContext = new CrearReporteModel();
        }

        private void btnAtras_clicked(object sender, EventArgs e)
        {
            ((NavigationPage)this.Parent).PushAsync(new Reportes());
        }
    }
}