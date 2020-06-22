using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using VYRMobile.Models;
using Rg.Plugins.Popup.Extensions;
using VYRMobile.Data;
using VYRMobile.Services;

namespace VYRMobile.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PickerPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        private ReportsStore _store { get; set; }
        public PickerPopup()
        {
            InitializeComponent();
            _store = new ReportsStore();
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            picker.ItemsSource = App.ExchangeableWorkers;
        }
        private async void Picker_OkButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            var selectedWorker = e.NewValue as ApplicationUser;

            if (selectedWorker != null)
            {
                await Navigation.PushPopupAsync(new LoadingPopup("Procesando..."));

                await SustituteWorker(App.WorkerOnReview, selectedWorker);

                await Navigation.PopPopupAsync();

                await Navigation.PopPopupAsync();
            }
            else
            {
                await Navigation.PopPopupAsync();
            }
        }

        private async void Picker_CancelButtonClicked(object sender, Syncfusion.SfPicker.XForms.SelectionChangedEventArgs e)
        {
            await Navigation.PopPopupAsync();
        }
        private async Task SustituteWorker(ApplicationUser OldWorker, ApplicationUser NewWorker)
        {
            bool WasOldWorkerSuccess = await _store.SetWorkerScheduleAsync(OldWorker, "Sustituido");
            bool WasNewWorkerSuccess = await _store.SetWorkerScheduleAsync(NewWorker, OldWorker.Schedule);

            if (WasOldWorkerSuccess == true || WasNewWorkerSuccess == true)
            {
                var oldNewUser = App.Workers.Find(p => p.Id.Equals(NewWorker.Id));
                var newNewUser = oldNewUser;
                newNewUser.Schedule = OldWorker.Schedule;

                App.Workers[App.Workers.FindIndex(ind => ind.Equals(oldNewUser))] = newNewUser;


                var oldOldUser = App.Workers.Find(p => p.Id.Equals(OldWorker.Id));
                var newOldUser = oldOldUser;
                newOldUser.Schedule = "Sustituido";

                App.Workers[App.Workers.FindIndex(ind => ind.Equals(oldOldUser))] = newOldUser;


                DependencyService.Get<IToast>().LongToast("Sustituido correctamente");
            }
            else
            {
                DependencyService.Get<IToast>().LongToast("Proceso fallido");
            }
        }
    }
}