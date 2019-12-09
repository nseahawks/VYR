using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Timers;
using System.Windows.Input;
using Xamarin.Forms;
using MvvmHelpers;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace VYRMobile.ViewModels
{
    public class CronoViewModel : INotifyPropertyChanged
    {
        
        public Stopwatch stopWatch = new Stopwatch();
        private Timer time = new Timer();

        public ICommand StopCommand { get; }
        public ICommand StartCommand { get; }
        public void StartStopwatch()
        {
            stopWatch.Restart();
        }
        public void StopStopwatch()
        {
            stopWatch.Stop();
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

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        
        public CronoViewModel()
        {
            

            
            stopWatch.Start();
            /*StopWatchHours = stopWatch.Elapsed.Hours.ToString();
            StopWatchMinutes = stopWatch.Elapsed.Minutes.ToString();
            StopWatchSeconds = stopWatch.Elapsed.Seconds.ToString();
            StopWatchMilliseconds = stopWatch.Elapsed.Milliseconds.ToString();*/

            Device.StartTimer(TimeSpan.FromMilliseconds(1), () =>
            {
                StopWatchHours = stopWatch.Elapsed.Hours.ToString("00");
                StopWatchMinutes = stopWatch.Elapsed.Minutes.ToString("00");
                StopWatchSeconds = stopWatch.Elapsed.Seconds.ToString("00");
                StopWatchMilliseconds = stopWatch.Elapsed.Milliseconds.ToString("000");

                return true;
            });
            //StopStopwatch();
            StopCommand = new Command(StopStopwatch);
            StartCommand = new Command(StartStopwatch);
        }

    }
}
