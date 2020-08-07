using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;
using System.Threading.Tasks;
using VYRMobile.Views;
using VYRMobile.Models;
using Plugin.Messaging;
using VYRMobile.Data;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms.GoogleMaps;
using Newtonsoft.Json;
using VYRMobile.Views.Popups;
using Plugin.CloudFirestore;
using CoordinateSharp;
using System.Collections.Generic;

namespace VYRMobile.ViewModels
{
    public class PuntoViewModel : INotifyPropertyChanged
    {
        private RecordsStore _store { get; set; }
        public Stopwatch stopWatch = new Stopwatch();
        static List<Antena> BackupLocations = new List<Antena>();
        public Command MapCommand { get; set; }
        public ICommand StopCommand { get; }
        public ICommand StartCommand { get; set; }
        public ICommand RestartCommand { get; set; }
        public ICommand AlertCommand { get; set; }
        public ICommand CheckAntenna { get; }
        public ICommand ItemSelectedCommand => new Command<string>(ItemSelected);

        private readonly static PuntoViewModel _instance = new PuntoViewModel();
        public static PuntoViewModel Instance
        {
            get
            {
                return _instance;
            }
        }
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
                OnPropertyChanged(nameof(Antenas));
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
        
        private bool _isEmpty;
        public bool IsEmpty
        {
            get { return _isEmpty; }
            set
            {
                _isEmpty = value;
                OnPropertyChanged(nameof(IsEmpty));
            }
        }
        private bool _isList;
        public bool IsList
        {
            get { return _isList; }
            set
            {
                _isList = value;
                OnPropertyChanged(nameof(IsList));
            }
        }
        private bool _isDoneMessage;
        public bool IsDoneMessage
        {
            get { return _isDoneMessage; }
            set
            {
                _isDoneMessage = value;
                OnPropertyChanged(nameof(IsDoneMessage));
            }
        }
        private bool _isButton;
        public bool IsButton
        {
            get { return _isButton; }
            set
            {
                _isButton = value;
                OnPropertyChanged(nameof(IsButton));
            }
        }
        private Color _chronoColor;
        public Color ChronoColor
        {
            get { return _chronoColor; }
            set
            {
                _chronoColor = value;
                OnPropertyChanged(nameof(ChronoColor));
            }
        }
        private int roundNumber = 1;
        public int RoundNumber
        {
            get { return roundNumber; }
            set
            {
                roundNumber = value;
                OnPropertyChanged(nameof(RoundNumber));
            }
        }
        private int maximumRounds;
        public int MaximumRounds
        {
            get { return maximumRounds; }
            set
            {
                maximumRounds = value;
                OnPropertyChanged(nameof(MaximumRounds));
            }
        }
        private string roundsDisplay;
        public string RoundsDisplay
        {
            get { return roundsDisplay; }
            set
            {
                roundsDisplay = value;
                OnPropertyChanged(nameof(RoundsDisplay));
            }
        }
        private static bool isTrueForAll(Antena antena)
        {
            return (antena.PointChecked == true);
        }
        private static bool isTrueForAllLocationNames(Antena antena)
        {
            return BackupLocations.Exists(p => p.LocationName.Equals(antena.LocationName));
        }

        public PuntoViewModel()
        {
            IsList = true;
            IsDoneMessage = false;
            IsButton = true;
            ChronoColor = Color.Black;

            _store = new RecordsStore();
            Puntos = new ObservableCollection<Punto>();
            //Tareas = new ObservableCollection<Report>();
            Antenas = new ObservableCollection<Antena>();

            LoadData();
            //LoadData2();

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
            RestartCommand = new Command(RestartStopwatch);
            CheckAntenna = new Command(CheckingAntenna);
            AlertCommand = new Command(Alert);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            {
            if (changed != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        private async void Alert() 
        {
            TimeSpan timeSpan = new TimeSpan();
            timeSpan = TimeSpan.FromSeconds(60);

            await App.Current.MainPage.Navigation.PopPopupAsync();

            await Task.Delay(100);
            IsButton = false;
            stopWatch.Start();
            ChronoColorChange(timeSpan);

            await App.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new Mapa2(App.Alarm.LocationName, App.Alarm.Location)));
        }

        private void StartStopwatch()
        {
            TimeSpan timeSpan = new TimeSpan();
            timeSpan = TimeSpan.FromSeconds(60); 

            if (stopWatch.ElapsedTicks != 0)
            {
                stopWatch.Restart();
            }
            else
            {
                stopWatch.Start();
            }

            ChronoColorChange(timeSpan);
        }
        private void RestartStopwatch()
        {
            stopWatch.Restart();
        }
        public void StopStopwatch()
        {
            if(stopWatch.IsRunning == true)
            {
                stopWatch.Stop();
            }
        }

        private async void CheckingAntenna()
        {
            bool wereChangesDone = false;
            foreach (var antena in Antenas)
            {
                string antenaId = antena.Id.ToString();

                if(antenaId == App.AntennaId)
                {
                    var userLocation = await Geolocation.GetLastKnownLocationAsync();
                    Location locationCoordinates = new Location(antena.Latitude, antena.Longitude);

                    var distance = Location.CalculateDistance(userLocation.Latitude, userLocation.Longitude, locationCoordinates, DistanceUnits.Kilometers);

                    if(distance <= 0.2)
                    {
                        antena.PointChecked = true;

                        foreach(var location in App.UserLocations)
                        {
                            if (location.Id.ToString() == antenaId)
                            {
                                location.PointChecked = true;
                            }
                        }

                        wereChangesDone = true;

                        CheckIfAllPointsAreDone();
                        /*await CrossCloudFirestore.Current
                            .Instance
                            .GetCollection("usersApp")
                            .GetDocument(App.ApplicationUserId)
                            .GetCollection("Stands")
                            .GetDocument(antenaId)
                            .UpdateDataAsync(new { Covered = true });*/

                        var record = new Record()
                        {
                            UserId = await SecureStorage.GetAsync("id"),
                            Type = "Punto verificado",
                            RecordType = Record.RecordTypes.AntennaCovered,
                            Owner = antena.LocationName,
                            Date = DateTime.Now,
                            Icon = "qr.png"
                        };

                        App.Records.Add(record);
                        var Records = App.Records;
                        var json = JsonConvert.SerializeObject(Records);
                        await SecureStorage.SetAsync("records", json);

                        await _store.AddRecordAsync(record);

                        await App.Current.MainPage.DisplayAlert("Completado", "Punto verificado correctamente", "Aceptar");
                        //await App.Current.MainPage.Navigation.PopModalAsync();

                        break;
                        //
                        //await App.Current.MainPage.DisplayAlert("Completado", "Punto verificado correctamente", "Aceptar");

                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Muy lejos", "No estas en el punto que quieres verificar", "Aceptar");
                        break;
                        //
                    }
                }
            }

            if(wereChangesDone)
            {
                var roundsStateBackupJson = JsonConvert.SerializeObject(App.UserLocations);
                await SecureStorage.SetAsync("roundsStateBackup", roundsStateBackupJson);
            }
        }
        private async void LoadData()
        {
            var antennas = await ReportsStore.Instance.GetAntenasAsync();
            var roundsStateBackup = await SecureStorage.GetAsync("roundsStateBackup");
            string roundsStateNumber = await SecureStorage.GetAsync("roundsStateNumberBackup");

            if(!string.IsNullOrEmpty(roundsStateNumber))
            {
                RoundNumber = Convert.ToInt32(roundsStateNumber);
            }

            if(roundsStateBackup != null)
            {
                BackupLocations = JsonConvert.DeserializeObject<List<Antena>>(roundsStateBackup);

                if(antennas.TrueForAll(isTrueForAllLocationNames))
                {
                    antennas = BackupLocations;
                }
            }

            App.UserLocations = antennas;
            Antenas.Clear();

            foreach (var antenna in antennas)
            {
                Antenas.Add(antenna);
            }

            if (Antenas.Count == 0)
            {
                IsEmpty = true;
            }
            else
            {
                IsEmpty = false;
            }

            DetermineNumberOfRounds();
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
        private async void ChronoColorChange(TimeSpan timeSpan)
        {
            while(stopWatch.IsRunning == true && timeSpan > stopWatch.Elapsed)
            {
                ChronoColor = Color.Green;
                await Task.Delay(1000);
                ChronoColor = Color.Black;
                await Task.Delay(1000);
            }
            
            while(stopWatch.IsRunning == true && timeSpan < stopWatch.Elapsed)
            {
                ChronoColor = Color.Red;
                await Task.Delay(1000);
                ChronoColor = Color.Black;
                await Task.Delay(1000);
            }
        }
        private async void CheckIfAllPointsAreDone()
        {
            bool areAllDone = App.UserLocations.TrueForAll(isTrueForAll);

            if(areAllDone)
            {
                foreach(var antena in Antenas)
                {
                    antena.PointChecked = false;
                    foreach(var location in App.UserLocations)
                    {
                        location.PointChecked = false;
                    }
                }

                if(RoundNumber < MaximumRounds)
                {
                    RoundNumber++;
                    RoundsDisplay = RoundNumber.ToString() + "/" + MaximumRounds.ToString();
                    await SecureStorage.SetAsync("roundsNumberStateBackup", RoundNumber.ToString());
                }
                else
                {
                    IsButton = false;
                    IsList = false;
                    IsDoneMessage = true;
                    await App.Current.MainPage.DisplayAlert("Completado", "Ya no tienes mas rondas por hoy", "Aceptar");
                }
            }
        }
        static public bool IsTimeOfDayBetween(DateTime time, TimeSpan startTime, TimeSpan endTime)
        {
            if (endTime == startTime)
            {
                return true;
            }
            else if (endTime < startTime)
            {
                return time.TimeOfDay <= endTime ||
                    time.TimeOfDay >= startTime;
            }
            else
            {
                return time.TimeOfDay >= startTime &&
                    time.TimeOfDay <= endTime;
            }
        }
        private void DetermineNumberOfRounds()
        {
            bool IsDayShift = IsTimeOfDayBetween(DateTime.Now, new TimeSpan(6, 0, 0), new TimeSpan(18, 0, 0));

            if (IsDayShift)
            {
                MaximumRounds = 4;
            }
            else
            {
                MaximumRounds = 2;
            }

            RoundsDisplay = RoundNumber.ToString() + "/" + MaximumRounds.ToString();
        }
    }
}
