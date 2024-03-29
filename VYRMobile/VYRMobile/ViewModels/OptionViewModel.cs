﻿using System.Collections.ObjectModel;
using System.Threading.Tasks;
using VYRMobile.Services;
using Xamarin.Forms;
using VYRMobile.Views;
using Rg.Plugins.Popup.Extensions;
using VYRMobile.Views.Popups;

namespace VYRMobile.ViewModels
{
    public class OptionViewModel : BindableObject
    {
        public Command ConfigCommand { get; set; }
        public Command EquipoCommand { get; set; }
        public Command ObjetivosCommand { get; set; }
        public Command FormacionCommand { get; set; }
        public Command CloseCommand { get; set; }
        public Command LogoutCommand { get; set; }
        public Command HistoryCommand { get; set; }

        private ObservableCollection<Models.Option> _options;

        public OptionViewModel()
        {
            Options = new ObservableCollection<Models.Option>();

            ConfigCommand = new Command(async () => await PushConfiguracion());
            EquipoCommand = new Command(async () => await PushEquipamiento());
            ObjetivosCommand = new Command(async () => await PushEstadisticas());
            FormacionCommand = new Command(async () => await PushFormacion());
            CloseCommand = new Command(async () => await ClosePage());
            LogoutCommand = new Command(async () => await LogoutUser());
            HistoryCommand = new Command(async () => await PushHistory());
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
            await App.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new ConfigurationPage())
            {
                BarBackgroundColor = (Color)Application.Current.Resources["PrimaryColor"],
                BarTextColor = (Color)Application.Current.Resources["SecondaryColor"],
            });
        }
        private async Task PushEquipamiento()
        {
            App.IsEquipmentValitated = true;
            await App.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new EquipmentPage())
            {
                BarBackgroundColor = (Color)Application.Current.Resources["PrimaryColor"],
                BarTextColor = (Color)Application.Current.Resources["SecondaryColor"],
            });
        }
        private async Task PushEstadisticas()
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new StatisticsPage())
            {
                BarBackgroundColor = (Color)Application.Current.Resources["PrimaryColor"],
                BarTextColor = (Color)Application.Current.Resources["SecondaryColor"],
            });
        }
        private async Task PushFormacion()
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new ElearningPage())
            {
                BarBackgroundColor = (Color)Application.Current.Resources["PrimaryColor"],
                BarTextColor = (Color)Application.Current.Resources["SecondaryColor"],
            });
        }
        private async Task PushHistory()
        {
            await App.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new HistoryPage())
            {
                BarBackgroundColor = (Color)Application.Current.Resources["PrimaryColor"],
                BarTextColor = (Color)Application.Current.Resources["SecondaryColor"],
            });
        }
        private async Task ClosePage()
        {
            await App.Current.MainPage.Navigation.PopModalAsync();
        }
        private async Task LogoutUser()
        {
            bool Logout;
            Logout = await App.Current.MainPage.DisplayAlert("Confirmación", "¿Desea cerrar la sesión?", "ACEPTAR", "CANCELAR");
            if (Logout)
            {
                await App.Current.MainPage.Navigation.PushPopupAsync(new LogingOutPopup());
            }
        }
    }
}
