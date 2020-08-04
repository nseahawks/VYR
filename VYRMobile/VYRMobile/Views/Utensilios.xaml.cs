using Plugin.CloudFirestore;
using Plugin.DeviceInfo;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using VYRMobile.Models;
using VYRMobile.ViewModels;
using VYRMobile.Views.Popups;
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
            BindingContext = EquipoViewModel.Instance;
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
            isAccepted = await DisplayAlert("Confirmación", "Confirmo que la información suministrada es comprobable y veraz", "ACEPTAR", "CANCELAR");
            if (isAccepted)
            {
                /*await CrossCloudFirestore.Current.Instance
                                          .GetCollection("usersApp")
                                          .GetDocument(App.ApplicationUserId)
                                          .UpdateDataAsync(new { Status = "WAITING" });

                List<Equipo> items = new List<Equipo>();
                
                items = itemsList.ItemsSource as List<Equipo>;
                
                foreach(var item in itemsList.ItemsSource)
                {
                    item.
                }
                var isEquipmentReady = isTrueForAll(itemsList.ItemsSource);*/

                bool isEquipmentReady = EquipoViewModel.Instance.getEquipos();

                if(isEquipmentReady)
                {
                    Application.Current.MainPage = new NavigationPage(new Loading());
                }
                else
                {
                    List<Equipo> items = EquipoViewModel.Instance.getMissingEquipment();
                    await Navigation.PushPopupAsync(new MissingEquipmentReport(items));
                }
            }
        }

    }
}