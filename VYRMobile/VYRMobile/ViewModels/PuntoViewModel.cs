using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Timers;
using MvvmHelpers;
using System.Windows.Input;
using VYRMobile.Services;
using Xamarin.Forms;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Threading.Tasks;
using VYRMobile.Views;
using VYRMobile.Models;
using Plugin.Messaging;
using VYRMobile.Data;
using Rg.Plugins.Popup.Extensions;
using VYRMobile.Views.Popups;
using Xamarin.Essentials;
using Xamarin.Forms.GoogleMaps;
using Newtonsoft.Json;
using System.IO;

namespace VYRMobile.ViewModels
{
    public class PuntoViewModel : INotifyPropertyChanged
    {
        public Stopwatch stopWatch = new Stopwatch();
        public Command MapCommand { get; set; }
        public ICommand StopCommand { get; }
        public ICommand StartCommand { get; }
        public ICommand CheckAntenna { get; }
        public ICommand ItemSelectedCommand => new Command<string>(ItemSelected);

        private string antenna;
        public string Antenna
        {
            get { return antenna; }
            set
            {
                antenna = value;
                OnPropertyChanged("Antenna");
            }
        }

        private string _stopWatchFinal;
        public string StopWatchFinal
        {
            get { return _stopWatchFinal; }
            set
            {
                _stopWatchFinal = value;
                OnPropertyChanged("StopWatchFinal");
            }
        }

        private string _stopWatchHours;
        public string StopWatchHours
        {
            get { return _stopWatchHours; }
            set
            {
                _stopWatchHours = value;
                OnPropertyChanged("StopWatchHours");
            }
        }
        private string _stopWatchMinutes;
        public string StopWatchMinutes
        {
            get { return _stopWatchMinutes; }
            set
            {
                _stopWatchMinutes = value;
                OnPropertyChanged("StopWatchMinutes");
            }
        }

        private string _stopWatchSeconds;
        public string StopWatchSeconds
        {
            get { return _stopWatchSeconds; }
            set
            {
                _stopWatchSeconds = value;
                OnPropertyChanged("StopWatchSeconds");
            }
        }

        private string _stopWatchMilliseconds;
        public string StopWatchMilliseconds
        {
            get { return _stopWatchMilliseconds; }
            set
            {
                _stopWatchMilliseconds = value;
                OnPropertyChanged("StopWatchMilliseconds");
            }
        }


        private ObservableCollection<Punto> _puntos;
        public ObservableCollection<Punto> Puntos
        {
            get { return _puntos; }
            set
            {
                _puntos = value;
                OnPropertyChanged("Puntos");
            }
        }
        private ObservableCollection<Antena> _antenas;
        public ObservableCollection<Antena> Antenas
        {
            get { return _antenas; }
            set
            {
                _antenas = value;
                OnPropertyChanged("Antenas");
            }
        }

        private ObservableCollection<Report> _tareas;
        public ObservableCollection<Report> Tareas
        {
            get { return _tareas; }
            set
            {
                _tareas = value;
                OnPropertyChanged("Tareas");
            }
        }

        public PuntoViewModel()
        {
            Puntos = new ObservableCollection<Punto>();
            Tareas = new ObservableCollection<Report>();
            Antenas = new ObservableCollection<Antena>();

            LoadData();
            LoadData2();

            //stopWatch.Start();

            Device.StartTimer(TimeSpan.FromMilliseconds(1), () =>
            {
                StopWatchHours = stopWatch.Elapsed.Hours.ToString("00");
                StopWatchMinutes = stopWatch.Elapsed.Minutes.ToString("00");
                StopWatchSeconds = stopWatch.Elapsed.Seconds.ToString("00");
                StopWatchMilliseconds = stopWatch.Elapsed.Milliseconds.ToString("000");

                if(StopWatchHours == "00" && StopWatchMinutes == "00")
                {
                    StopWatchFinal = (StopWatchSeconds +
                                   "." + StopWatchMilliseconds);
                }
                else if (StopWatchHours == "00")
                {
                    StopWatchFinal = (StopWatchMinutes +
                                  ":" + StopWatchSeconds +
                                  "." + StopWatchMilliseconds);
                }
                else
                {
                    StopWatchFinal = (StopWatchHours +
                                   ":" + StopWatchMinutes +
                                   ":" + StopWatchSeconds +
                                   "." + StopWatchMilliseconds);
                }

                return true;
            });

            StopCommand = new Command(StopStopwatch);
            StartCommand = new Command(Alert);
            CheckAntenna = new Command(CheckingAntenna);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private async void Alert() 
        {
            stopWatch.Start();
            await App.Current.MainPage.Navigation.PopPopupAsync();
            await ShowMap();
            await GetActualLocation();
            GoogleMapsViewModel.Instance.LoadRouteCommand2.Execute(null);
            //MapCommand.Execute(null);
        }

        public void StartStopwatch()
        {
            stopWatch.Restart();
        }
        public void StopStopwatch()
        {
            stopWatch.Stop();
        }

        private async void CheckingAntenna()
        {
            var antenas = Antenas;

            foreach (var antena in antenas)
            {
                string antenaId = antena.Id.ToString();

                if (antenaId == Antenna)
                {
                    antena.PointChecked = true;

                    var record = new Record()
                    {
                        UserId = await SecureStorage.GetAsync("id"),
                        Type = "Antena Cubierta",
                        RecordType = Record.RecordTypes.AntennaCovered,
                        Owner = antena.LocationName,
                        Date = DateTime.Now,
                        Icon = "qr.png"
                    };

                    App.Records.Add(record);
                    var Records = App.Records;
                    var json = JsonConvert.SerializeObject(Records);
                    Application.Current.Properties["record"] = json;

                    break;
                }
            }
        }
        private async void LoadData()
        {
            var antennas = await ReportsStore.Instance.GetAntenasAsync();
            Antenas.Clear();
            foreach (var antenna in antennas)
            {
                Antenas.Add(antenna);
            }
        }
        private async void LoadData2()
        {
            var tareas = await ReportsStore.Instance.GetReportsAsync();
            Tareas.Clear();
            foreach (var tarea in tareas)
            {
                if (tarea.ReportType == Report.ReportTypes.Daño & tarea.ReportStatus == Report.ReportStatuses.Abierto)
                {
                    Tareas.Add(tarea);
                }
            }
        }
        private void ItemSelected(string parameter)
        {
            var phoneCallTask = CrossMessaging.Current.PhoneDialer;
            if (phoneCallTask.CanMakePhoneCall)
            {
                phoneCallTask.MakePhoneCall("+18097966316", "Francisco Rojas");
            }
        }
        private async Task GetActualLocation()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();
                Position position = new Position(location.Latitude, location.Longitude);

                if (location != null)
                {
                    Mapa2 mapa = new Mapa2();
                    mapa.OriginLocationlat = position.Latitude.ToString();
                    mapa.OriginLocationlng = position.Longitude.ToString();
                    mapa.DestinationLocationlat = "18.4047";
                    mapa.DestinationLocationlng = "-70.0328";
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"No es posible obtener tu ubicacion {ex.Message}", "Ok");
            }
        }
        private async Task ShowMap()
        {
            await App.Current.MainPage.Navigation.PushAsync(MenuPage.Instance);
            MenuPage.Instance.ShowMapCommand.Execute(null);
            /*var menuPage = new MenuPage(); 
            MenuPage.Instance.CurrentPage = MenuPage.Instance.Children[1];
            await menuPage.CurrentPage.Navigation.PushAsync(new Mapa2());
            App.Current.MainPage = new NavigationPage(menuPage);
            MenuPage menupage = new MenuPage();
            var pages = menupage.Children.GetEnumerator();
            pages.Reset();
            pages.MoveNext();
            pages.MoveNext();

            await App.Current.MainPage.Navigation.PushAsync(menupage);*/
        }
    }
}
