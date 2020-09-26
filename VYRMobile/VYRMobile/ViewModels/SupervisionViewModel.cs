using MvvmHelpers;
using System;
using System.Collections.ObjectModel;
using VYRMobile.Data;
using VYRMobile.Models;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VYRMobile.ViewModels
{
    public class SupervisionViewModel : ObservableObject
    {
        private readonly static SupervisionViewModel _instance = new SupervisionViewModel();
        public static SupervisionViewModel Instance
        {
            get
            {
                return _instance;
            }
        }
        private ObservableCollection<ApplicationUser> _workers;
        public ObservableCollection<ApplicationUser> Workers
        {
            get { return _workers; }
            set
            {
                _workers = value;
                OnPropertyChanged();
            }
        }
        public SupervisionViewModel()
        {
            Workers = new ObservableCollection<ApplicationUser>();

            GetWorkers();
        }
        private async void GetWorkers()
        {
            try
            {
                var workers = await ReportsStore.Instance.GetUsersAsync();

                App.Workers.Clear();
                App.ExchangeableWorkers.Clear();

                foreach (var worker in workers)
                {
                    var lastEvaluatedDate = await SecureStorage.GetAsync("lastEvaluatedDate");
                    worker.FullName = worker.FirstName + " " + worker.LastName;

                    if (App.ExchangeableWorkers.Contains(worker) == false && worker.Exchange == true)
                    {
                        App.ExchangeableWorkers.Add(worker);
                    }

                    if (lastEvaluatedDate != DateTime.Now.ToString("dd/MM/yyyy"))
                    {
                        worker.IsAssist = false;
                        App.Workers.Add(worker);
                        Workers.Add(worker);
                    }
                    else
                    {
                        App.Workers.Add(worker);
                        Workers.Add(worker);
                    }
                }

                await SecureStorage.SetAsync("lastEvaluatedDate", DateTime.Now.ToString("dd/MM/yyyy"));
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Error", "No se puede procesar la informacion en este momento", "Aceptar");
            }
        }
    }
}
