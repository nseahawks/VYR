using Newtonsoft.Json;
using Plugin.CloudFirestore;
using Plugin.Messaging;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using VYRMobile.Data;
using VYRMobile.Helper;
using VYRMobile.Models;
using VYRMobile.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace VYRMobile.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        private RecordsStore _store { get; set; }
        public Stopwatch stopWatch = new Stopwatch();
        static List<CompanyLocation> BackupLocations = new List<CompanyLocation>();
        PermissionsHelper _permissions = new PermissionsHelper();
        public Command MapCommand { get; set; }
        public ICommand StopCommand { get; }
        public ICommand StartCommand { get; set; }
        public ICommand RestartCommand { get; set; }
        public ICommand AlertCommand { get; set; }
        public ICommand CheckAntenna { get; }
        public ICommand ItemSelectedCommand => new Command<string>(ItemSelected);

        private readonly static HomeViewModel _instance = new HomeViewModel();
        public static HomeViewModel Instance
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

        private ObservableCollection<CompanyLocation> _antenas;
        public ObservableCollection<CompanyLocation> Antenas
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
        private static bool isTrueForAll(CompanyLocation antena)
        {
            return (antena.PointChecked == true);
        }
        private static bool isTrueForAllLocationNames(CompanyLocation antena)
        {
            return BackupLocations.Exists(p => p.LocationName.Equals(antena.LocationName));
        }

        public HomeViewModel()
        {
            IsList = true;
            IsDoneMessage = false;
            IsButton = true;
            ChronoColor = Color.Black;

            _store = new RecordsStore();
            //Tareas = new ObservableCollection<Report>();
            Antenas = new ObservableCollection<CompanyLocation>();

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

            await App.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new MapPage(App.Alarm.LocationName, App.Alarm.Location)));
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
                    bool isLocationPermited = await _permissions.CheckLocationPermissionsStatus();

                    if (isLocationPermited)
                    {
                        try
                        {
                            var userLocation = await Geolocation.GetLastKnownLocationAsync();
                            Location locationCoordinates = new Location(antena.Latitude, antena.Longitude);

                            var distance = Location.CalculateDistance(userLocation.Latitude, userLocation.Longitude, locationCoordinates, DistanceUnits.Kilometers);

                            if (distance <= 0.2)
                            {
                                antena.PointChecked = true;

                                foreach (var location in App.UserLocations)
                                {
                                    if (location.Id.ToString() == antenaId)
                                    {
                                        location.PointChecked = true;
                                    }
                                }

                                wereChangesDone = true;

                                CheckIfAllPointsAreDone();
                                await CrossCloudFirestore.Current
                                    .Instance
                                    .GetCollection("Stands")
                                    .GetDocument(App.ApplicationUserId)
                                    .GetCollection("Locations")
                                    .GetDocument(antenaId)
                                    .UpdateDataAsync(new { IsChecked = true });

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

                                break;
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("Muy lejos", "No estás en el punto que quieres verificar", "Aceptar");
                                break;
                            }
                        }
                        catch
                        {
                            await App.Current.MainPage.DisplayAlert("Error", "Ocurrió un problema al procesar la información", "Aceptar");
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Fallido", "Por favor activa tu GPS para continuar", "Aceptar");
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

            var backupLocationsData = await SecureStorage.GetAsync("roundsStateBackup");
            List<CompanyLocation> backupLocationsList = JsonConvert.DeserializeObject<List<CompanyLocation>>(backupLocationsData);
            string roundsStateNumber = await SecureStorage.GetAsync("roundsStateNumberBackup");

            if(!string.IsNullOrEmpty(roundsStateNumber))
            {
                RoundNumber = Convert.ToInt32(roundsStateNumber);
            }

            if(backupLocationsList != null)
            {
                foreach (var location in backupLocationsList)
                {
                    BackupLocations.Add(location);
                }
                
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

                        await CrossCloudFirestore.Current
                            .Instance
                            .GetCollection("Stands")
                            .GetDocument(App.ApplicationUserId)
                            .GetCollection("Locations")
                            .GetDocument(location.Id.ToString())
                            .UpdateDataAsync(new { IsChecked = false });
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
        private async Task<IEnumerable<CompanyLocation>> GetBackupLocations()
        {
            var items = await CrossCloudFirestore.Current.Instance
                                        .GetCollection("Stands")
                                        .GetDocument(App.ApplicationUserId)
                                        .GetCollection("Locations")
                                        .GetDocumentsAsync();
            
            IEnumerable<CompanyLocation> itemslist = items.ToObjects<CompanyLocation>();

            return itemslist;
        }
    }
}
