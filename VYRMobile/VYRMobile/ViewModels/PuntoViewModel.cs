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

namespace VYRMobile.ViewModels
{
    public class PuntoViewModel : INotifyPropertyChanged
    {
        private RecordsStore _store { get; set; }
        public Stopwatch stopWatch = new Stopwatch();
        public Command MapCommand { get; set; }
        public ICommand StopCommand { get; }
        public ICommand StartCommand { get; set; }
        public ICommand RestartCommand { get; set; }
        public ICommand AlertCommand { get; set; }
        public ICommand CheckAntenna { get; }
        public ICommand ItemSelectedCommand => new Command<string>(ItemSelected);

        private static PuntoViewModel _instance;
        public static PuntoViewModel Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PuntoViewModel();

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
        private static bool isTrueForAll(Antena antena)
        {
            return (antena.PointChecked == true);
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
            //ShowMap();
            //await GetActualLocation();
            //GoogleMapsViewModel.Instance.LoadRouteCommand2.Execute(null);
            //MapCommand.Execute(null);
        }

        private void StartStopwatch()
        {
            /*FirestoreAlarm alarmDocument = new FirestoreAlarm()
            {
                LocationName = "Seahawks",
                Location = new GeoPoint(18.4047, -70.0328),
                Type = "Alarm"
            };
            await App.Current.MainPage.Navigation.PushPopupAsync(new AlarmPopup(alarmDocument.LocationName, alarmDocument.Location, alarmDocument.Type));*/

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
            foreach (var antena in Antenas)
            {
                string antenaId = antena.Id.ToString();

                if(antenaId == App.AntennaId)
                {
                    //var userLocation = await Geolocation.GetLastKnownLocationAsync();
                    //Location locationCoordinates = new Location(antena.Latitude, antena.Longitude);

                    //var distance = Location.CalculateDistance(userLocation.Latitude, userLocation.Longitude, locationCoordinates, DistanceUnits.Kilometers);

                    //if(distance <= 0.2)
                    //{
                        var oldAntena = App.UserLocations.Find(ant => ant.Equals(antena));
                        antena.PointChecked = true;
                        App.UserLocations.Remove(oldAntena);
                        App.UserLocations.Add(antena);
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
                            Type = "Antena Cubierta",
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

                        await App.Current.MainPage.Navigation.PopModalAsync();

                        break;
                        //
                        //await App.Current.MainPage.DisplayAlert("Completado", "Punto verificado correctamente", "Aceptar");

                    //}
                    /*else
                    {
                        await App.Current.MainPage.Navigation.PopModalAsync();
                        break;
                        //
                    }*/
                }
            }
        }
        private async void LoadData()
        {
            var antennas = await ReportsStore.Instance.GetAntenasAsync();
            App.UserLocations = antennas;
            Antenas.Clear();

            foreach (var antenna in antennas)
            {
                Antenas.Add(antenna);
            }

            if(Antenas.Count == 0)
            {
                IsEmpty = true;
            }
            else
            {
                IsEmpty = false;
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
        private void CheckIfAllPointsAreDone()
        {
            bool areAllDone = App.UserLocations.TrueForAll(isTrueForAll);

            if (areAllDone)
            {
                foreach(var antena in Antenas)
                {
                    antena.PointChecked = false;
                }

                if (RoundNumber < 4)
                {
                    RoundNumber++;
                }
                else
                {
                    IsButton = false;
                    IsList = false;
                    IsDoneMessage = true;
                }
            }
        }
        /*private async Task GetActualLocation()
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
                    mapa.DestinationLocationlat = App.Alarm.Location.Latitude.ToString();
                    mapa.DestinationLocationlng = App.Alarm.Location.Longitude.ToString();
                    //mapa.DestinationLocationlat = "18.4047";
                    //mapa.DestinationLocationlng = "-70.0328";
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"No es posible obtener tu ubicacion {ex.Message}", "Ok");
            }
        }*/
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
