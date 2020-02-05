using Plugin.CloudFirestore;
using Plugin.DeviceInfo;
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
        bool isAccepted;
        public Utensilios()
        {
            InitializeComponent();
            BindingContext = new EquipoViewModel();
            btnConfirmar.Clicked += BtnConfirmar_Clicked;
        }

        private async void BtnConfirmar_Clicked(object sender, EventArgs e)
        {
            isAccepted = await DisplayAlert("Confirmación", "¿Acepta los términos y condiciones de uso?", "ACEPTAR", "CANCELAR");
            if (isAccepted)
            {
                string _appId = CrossDeviceInfo.Current.Id;
                var id = CrossCloudFirestore.Current.Instance
                                 .GetCollection("usersApp")
                                 .GetDocument(_appId).Id;

                await CrossCloudFirestore.Current.Instance
                                          .GetCollection("usersApp")
                                          .GetDocument(_appId).SetDataAsync(new { Status = "ACCEPTED"});
               
                Application.Current.MainPage = new NavigationPage(new Loading());
            }
        }
    }
}