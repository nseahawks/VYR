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
    public partial class Usuario : ContentPage
    {
        public Command ConfigCommand { get; set; }
        public Command EquipoCommand { get; set; }
        public Command ObjetivosCommand { get; set; }
        public Command FormacionCommand { get; set; }
        public Usuario()
        {
            InitializeComponent();

            ConfigCommand = new Command(async () => await PushConfiguracion());
            EquipoCommand = new Command(async () => await PushEquipamiento());
            ObjetivosCommand = new Command(async () => await PushEstadisticas());
            FormacionCommand = new Command(async () => await PushFormacion());
        }

        private async Task PushConfiguracion()
        {
            await Navigation.PushAsync(new ConfiguracionPage());
        }
        private async Task PushEquipamiento()
        {
            await Navigation.PushAsync(new Utensilios());
        }
        private async Task PushEstadisticas()
        {
            await Navigation.PushAsync(new Estadisticas());
        }
        private async Task PushFormacion()
        {
            await Navigation.PushAsync(new Elearning());
        }
    }
}