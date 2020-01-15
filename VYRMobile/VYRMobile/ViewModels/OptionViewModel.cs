using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.Services;
using Xamarin.Forms;
using VYRMobile.Views;

namespace VYRMobile.ViewModels
{
    public class OptionViewModel : BindableObject
    {
        public Command ConfigCommand { get; set; }
        public Command EquipoCommand { get; set; }
        public Command ObjetivosCommand { get; set; }
        public Command FormacionCommand { get; set; }
        public Command CloseCommand { get; set; }

        private ObservableCollection<Models.Option> _options;

        public OptionViewModel()
        {
            Options = new ObservableCollection<Models.Option>();

            ConfigCommand = new Command(async () => await PushConfiguracion());
            EquipoCommand = new Command(async () => await PushEquipamiento());
            ObjetivosCommand = new Command(async () => await PushEstadisticas());
            FormacionCommand = new Command(async () => await PushFormacion());
            CloseCommand = new Command(async () => await ClosePage());
            LoadData();
        }
        public ObservableCollection<Models.Option> Options
        {
            get { return _options; }
            set
            {
                _options = value;
                OnPropertyChanged();
            }
        }
        private void LoadData()
        {
            var options = OptionService.Instance.GetOptions();
            Options.Clear();
            foreach (var option in options)
            {
                Options.Add(option);
            }
        }
        private async Task PushConfiguracion()
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new Configuracion()));
        }
        private async Task PushEquipamiento()
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new Utensilios()));
        }
        private async Task PushEstadisticas()
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new StatisticsPage()));
        }
        private async Task PushFormacion()
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new Elearning()));
        }
        private async Task ClosePage()
        {
            await App.Current.MainPage.Navigation.PopModalAsync();
        }
    }
}
