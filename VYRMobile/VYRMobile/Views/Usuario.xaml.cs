using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views
{
    public partial class Usuario : ContentPage
    {
        OptionViewModel VM = new OptionViewModel();
        public Usuario()
        {
            InitializeComponent();
            BindingContext = new OptionViewModel();
            GetUserInfo();
        }
        private async void GetUserInfo()
        {
            idLbl.Text = await SecureStorage.GetAsync("email");
        }

        private void configuracionCell_Tapped(object sender, EventArgs e)
        {
            VM.ConfigCommand.Execute(null);
        }

        private void equipamientoCell_Tapped(object sender, EventArgs e)
        {
            VM.EquipoCommand.Execute(null);
        }

        private void estadisticasCell_Tapped(object sender, EventArgs e)
        {
            VM.ObjetivosCommand.Execute(null);
        }

        private void formacionCell_Tapped(object sender, EventArgs e)
        {
            VM.FormacionCommand.Execute(null);
        }

        private void logoutCell_Tapped(object sender, EventArgs e)
        {
            VM.LogoutCommand.Execute(null);
        }
    }
}