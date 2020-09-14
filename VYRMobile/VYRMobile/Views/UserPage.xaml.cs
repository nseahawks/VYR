using System;
using VYRMobile.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VYRMobile.Views
{
    public partial class UserPage : ContentPage
    {
        OptionViewModel VM = new OptionViewModel();
        public UserPage()
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
        private void historyCell_Tapped(object sender, EventArgs e)
        {
            VM.HistoryCommand.Execute(null);
        }
    }
}