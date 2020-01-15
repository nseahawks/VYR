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

namespace VYRMobile.ViewModels
{
    public class PuntoViewModel : INotifyPropertyChanged
    {
        public Stopwatch stopWatch = new Stopwatch();

        public Command MapCommand { get; set; }
        public ICommand StopCommand { get; }
        public ICommand StartCommand { get; }
        public ICommand CheckAntenna { get; }

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

        private ObservableCollection<Models.Tarea> _tareas;
        public ObservableCollection<Models.Tarea> Tareas
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
            Puntos = new ObservableCollection<Models.Punto>();
            Tareas = new ObservableCollection<Models.Tarea>();

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
            await App.Current.MainPage.DisplayAlert("Alerta", "ALARMA SEAHAWKS", "ACEPTAR");
            StartStopwatch();
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
            var puntos = Puntos;

            foreach (var punto in puntos)
            {
                string pointId = punto.PointId.ToString();

                if (pointId == Antenna)
                {
                    punto.PointChecked = true;
                }
            }
        }
        private async void LoadData()
        {
            var puntos = await PuntoService.Instance.GetPuntos();
            Puntos.Clear();
            foreach (var punto in puntos)
            {
                Puntos.Add(punto);
            }
        }
        private async void LoadData2()
        {
            var tareas = await TareaService.Instance.GetTareas();
            Tareas.Clear();
            foreach (var tarea in tareas)
            {
                Tareas.Add(tarea);
            }
        }
    }
}
