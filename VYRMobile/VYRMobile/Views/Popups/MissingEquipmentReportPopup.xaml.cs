using Rg.Plugins.Popup.Extensions;
using Syncfusion.XForms.Buttons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.Data;
using VYRMobile.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MissingEquipmentReportPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public MissingEquipmentReportPopup(List<EquipmentItem> items)
        {
            InitializeComponent();

            GenerateChips(items);
            sendBtn.Clicked += SendBtn_Clicked;
        }

        private async void SendBtn_Clicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(justificacionEditor.Text))
            {
                await DisplayAlert("Incompleto", "Escribe tu justificación para continuar", "Aceptar");
                return;
            }

            string description = "";

            foreach(var itemName in App.ChipsNames)
            {                
                description = description + itemName + ", ";
            }
            Report reporte = new Report() 
            {
                Title = "Equipo incompleto",
                Description = "Articulos marcados como faltantes: " + description + Environment.NewLine + "Justificacion del trabajador: " + Environment.NewLine + justificacionEditor.Text,
                Created = DateTime.Now
            };
            await Navigation.PushPopupAsync(new LoadingPopup("Cargando..."));
            bool isSuccess = await ReportsStore.Instance.SendEventualityReportAsync(reporte);

            if (isSuccess)
            {
                await Navigation.PopAllPopupAsync();
                Application.Current.MainPage = new NavigationPage(new LoadingPage());
            }
            else
            {
                await Navigation.PopAllPopupAsync();
                await DisplayAlert("Fallido", "Hubo un problema al conectar con el servidor", "Aceptar");
            }
        }

        private void GenerateChips(List<EquipmentItem> items)
        {
            foreach(var item in items)
            {
                equipmentChips.Items.Add(new SfChip() { Text = item.Name, BackgroundColor = Color.FromHex("#DD0808") });
                App.ChipsNames.Add(item.Name);
            }
        }
    }
}