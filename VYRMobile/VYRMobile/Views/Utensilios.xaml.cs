using Plugin.CloudFirestore;
using Plugin.DeviceInfo;
using System;
using VYRMobile.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VYRMobile.Views
{
    public partial class Utensilios : ContentPage
    {
        bool isAccepted;
        public Utensilios()
        {
            InitializeComponent();
            BindingContext = new EquipoViewModel();
            btnConfirmar.Clicked += BtnConfirmar_Clicked;

            if (App.IsEquipmentValitated == true)
            {
                btnConfirmar.IsVisible = false;
                btnConfirmar.IsEnabled = false;
                titleLabel.Text = "Equipo seleccionado";
            }
        }

        private async void BtnConfirmar_Clicked(object sender, EventArgs e)
        {
            isAccepted = await DisplayAlert("Confirmación", "¿Acepta los términos y condiciones de uso?", "ACEPTAR", "CANCELAR");
            if (isAccepted)
            {
                string _userId = await SecureStorage.GetAsync("id");

                await CrossCloudFirestore.Current.Instance
                                          .GetCollection("usersApp")
                                          .GetDocument(_userId)
                                          .UpdateDataAsync(new { Status = "WAITING" });

                Application.Current.MainPage = new NavigationPage(new Loading());
            }
        }

    }
}