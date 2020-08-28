using Android.Content;
using Newtonsoft.Json;
using Plugin.Messaging;
using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.Helper;
using VYRMobile.Models;
using VYRMobile.Services;
using VYRMobile.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CallPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        PermissionsHelper _permissions = new PermissionsHelper();
        public CallPopup()
        {
            InitializeComponent();

            call1.Clicked += call1_Clicked;
            call2.Clicked += call2_Clicked;
        }

        private async void call1_Clicked(object sender, EventArgs e)
        {
            bool isCallPermited = await _permissions.CheckPhonecallPermissionsStatus();

            if (isCallPermited)
            {
                try
                {
                    string phoneNumber = "+1911";
                    DependencyService.Get<IMakePhoneCall>().MakeCall(phoneNumber);

                    var record = new Record()
                    {
                        UserId = await SecureStorage.GetAsync("id"),
                        Type = "Llamada",
                        RecordType = Record.RecordTypes.AntennaCovered,
                        Owner = "Emergencias",
                        Date = DateTime.Now,
                        Icon = "callM.png"
                    };

                    App.Records.Add(record);
                    var Records = App.Records;
                    var json = JsonConvert.SerializeObject(Records);
                    await SecureStorage.SetAsync("records", json);
                }
                catch
                {
                    await DisplayAlert("Error", "Ocurrió un problema al intentar realizar la llamada", "Aceptar");
                }
            }
            else
            {
                await DisplayAlert("Fallido", "Otorga los permisos del teléfono para continuar", "Aceptar");
            }
        }
        private async void call2_Clicked(object sender, EventArgs e)
        {
            bool isCallPermited = await _permissions.CheckPhonecallPermissionsStatus();

            if (isCallPermited)
            {
                try
                {
                    string phoneNumber = "+18292926529";
                    DependencyService.Get<IMakePhoneCall>().MakeCall(phoneNumber);

                    var record = new Record()
                    {
                        UserId = await SecureStorage.GetAsync("id"),
                        Type = "Llamada",
                        RecordType = Record.RecordTypes.AntennaCovered,
                        Owner = "Monitoreo",
                        Date = DateTime.Now,
                        Icon = "callM.png"
                    };

                    App.Records.Add(record);
                    var Records = App.Records;
                    var json = JsonConvert.SerializeObject(Records);
                    await SecureStorage.SetAsync("records", json);
                }
                catch
                {
                    await DisplayAlert("Error", "Ocurrió un problema al intentar realizar la llamada", "Aceptar");
                }
            }
            else
            {
                await DisplayAlert("Fallido", "Otorga los permisos del teléfono para continuar", "Aceptar");
            }
        }
    }
}