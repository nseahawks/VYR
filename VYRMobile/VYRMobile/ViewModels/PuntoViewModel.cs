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


        private ObservableCollection<Models.Punto> _puntos;
        public ObservableCollection<Models.Punto> Puntos
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
                }else if (StopWatchHours == "00")
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
            StartStopwatch();
            await App.Current.MainPage.DisplayAlert("Alerta", "ALARMA SEAHAWKS", "ACEPTAR");
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
    }
}
