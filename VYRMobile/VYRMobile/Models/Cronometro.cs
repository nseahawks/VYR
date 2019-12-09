using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;

namespace VYRMobile.Models
{
    public class Cronometro : ObservableObject
    {
        public Stopwatch stopWatch = new Stopwatch();

        private string _hours;
        private string _minutes;
        private string _seconds;
        private string _milliseconds;

        public string Hours
        {
            get => _hours;
            set => SetProperty(ref _hours, value);
        }

        public string Minutes
        {
            get => _minutes;
            set => SetProperty(ref _minutes, value);
        }

        public string Seconds
        {
            get => _seconds;
            set => SetProperty(ref _seconds, value);
        }

        public string Milliseconds
        {
            get => _milliseconds;
            set => SetProperty(ref _milliseconds, value);
        }

        public Cronometro()
        {
            Stopwatch();
        }

        private void Stopwatch()
        {
            stopWatch.Start();
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                Hours = stopWatch.Elapsed.Hours.ToString();
                Minutes = stopWatch.Elapsed.Minutes.ToString();
                Seconds = stopWatch.Elapsed.Seconds.ToString();
                Milliseconds = stopWatch.Elapsed.Milliseconds.ToString();
                return true;
            });
        }

    }
}
