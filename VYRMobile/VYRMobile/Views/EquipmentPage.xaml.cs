using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using VYRMobile.Models;
using VYRMobile.ViewModels;
using VYRMobile.Views.Popups;
using Xamarin.Forms;

namespace VYRMobile.Views
{
    public partial class EquipmentPage : ContentPage
    {
        bool isAccepted;
        public EquipmentPage()
        {
            InitializeComponent();
            BindingContext = EquipmentViewModel.Instance;
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
            
                bool isEquipmentReady = EquipmentViewModel.Instance.getEquipos();

                if(isEquipmentReady)
                {
                    Application.Current.MainPage = new NavigationPage(new LoadingPage());
                }
                else
                {
                    List<EquipmentItem> items = EquipmentViewModel.Instance.getMissingEquipment();
                    await Navigation.PushPopupAsync(new MissingEquipmentReportPopup(items));
                }
            }
        }

        private void optionSwitch_Toggled(object sender, ToggledEventArgs e)
        {
            var switchObject = sender as Switch;

            switch (switchObject.IsToggled)
            {
                case true:
                    EquipmentViewModel.Instance.ToggleColor = Color.FromHex("#01BD00");
                    break;
                case false:
                    EquipmentViewModel.Instance.ToggleColor = Color.FromHex("#DD0808");
                    break;
            }
        }
    }
}