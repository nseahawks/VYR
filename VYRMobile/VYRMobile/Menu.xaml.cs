using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Menu : ContentPage
    {
        public Menu()
        {
            InitializeComponent();
        }

        private async void ConfiguracionClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new Configuracion());
        }
        private async void HistorialClicked(object sender, EventArgs args)
        {
            await Navigation.PushAsync(new Historial());
        }
    }
}