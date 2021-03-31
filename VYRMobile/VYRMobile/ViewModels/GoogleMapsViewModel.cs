using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using VYRMobile.Models;
using VYRMobile.Helper;
using VYRMobile.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using MvvmHelpers;
using VYRMobile.Data;
using VYRMobile.Views;
using Newtonsoft.Json;
using System.Windows.Input;
using Rg.Plugins.Popup.Extensions;
using System.Reflection;
using System.Collections.Generic;

namespace VYRMobile.ViewModels
{
    public class GoogleMapsViewModel : BaseViewModel
    {
        public Stopwatch stopWatch = new Stopwatch();
        PermissionsHelper _permissions = new PermissionsHelper();
        private RecordsStore _store { get; set; }
        public Command CalculateRouteCommand { get; set; }
        public Command UpdatePositionCommand { get; set; }
        public Command LoadRouteCommand { get; set; }
        public Command LoadRouteCommand2 { get; set; }
        public Command StopRouteCommand { get; set; }
        public Command ActualLocationCommand { get; set; }
        public ICommand StopCommand { get; }
        public ICommand StartCommand { get; set; }
        public ICommand RestartCommand { get; set; }
        
        SensorSpeed speed = SensorSpeed.Default;
        private static GoogleMapsViewModel _instance;
        public static GoogleMapsViewModel Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new GoogleMapsViewModel();

                return _instance;
            }
        }
        private string accelerometerData;
        public string AccelerometerData
        {
            get
            {
                return accelerometerData;
            }
            set
            {
                if (Accelerometer.IsMonitoring)
                {
                    if (value != null)
                    {
                        accelerometerData = value;
                        OnPropertyChanged("AccelerometerData");
                    }
                }
            }
        }

        IGoogleMapsApiService googleMapsApi = new GoogleMapsApiService();
        private bool hasRouteRunning;
        public bool HasRouteRunning
        {
            get
            {
                return hasRouteRunning;
            }
            set
            {
                if (hasRouteRunning != value)
                {
                    hasRouteRunning = value;
                    OnPropertyChanged("HasRouteRunning");
                }
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
        public string _originLatitud { get; set; }
        public string _originLongitud { get; set; }
        public string _destinationLatitud { get; set; }
        public string _destinationLongitud { get; set; }

        GooglePlaceAutoCompletePrediction _placeSelected;
        public GooglePlaceAutoCompletePrediction PlaceSelected
        {
            get
            {
                return _placeSelected;
            }
            set
            {
                _placeSelected = value;
                if (_placeSelected != null)
                    GetPlaceDetailCommand.Execute(_placeSelected);
            }
        }
        public Command FocusOriginCommand { get; set; }
        public Command GetPlacesCommand { get; set; }
        public Command GetPlaceDetailCommand { get; set; }

        public ObservableCollection<GooglePlaceAutoCompletePrediction> Places { get; set; }
        public ObservableCollection<GooglePlaceAutoCompletePrediction> RecentPlaces { get; set; } = new ObservableCollection<GooglePlaceAutoCompletePrediction>();

        public bool ShowRecentPlaces { get; set; }
        //bool _isPickupFocused = true;

        //string _pickupText;
        //public string PickupText
        //{
        //    get
        //    {
        //        return _pickupText;
        //    }
        //    set
        //    {
        //        _pickupText = value;
        //        if (!string.IsNullOrEmpty(_pickupText))
        //        {
        //            _isPickupFocused = true;
        //            GetPlacesCommand.Execute(_pickupText);
        //        }
        //    }
        //}

        //string _originText;
        //public string OriginText
        //{
        //    get
        //    {
        //        return _originText;
        //    }
        //    set
        //    {
        //        _originText = value;
        //        if (!string.IsNullOrEmpty(_originText))
        //        {
        //            _isPickupFocused = true;
        //            GetPlacesCommand.Execute(_originText);
        //        }
        //    }
        //}
        

        public Command GetLocationNameCommand { get; set; }
        public bool IsRouteNotRunning
        {
            get
            {
                return !HasRouteRunning;
            }
        }

        private ObservableCollection<CompanyLocation> _antennas;
        public ObservableCollection<CompanyLocation> Antennas
        {
            get { return _antennas; }
            set
            {
                _antennas = value;
                OnPropertyChanged();
            }
        }
        public GoogleMapsViewModel()
        {
            _store = new RecordsStore();
            Antennas = new ObservableCollection<CompanyLocation>();
            ToggleAccelerometer();
            LoadRouteCommand = new Command(async () => await LoadRoute());
            LoadRouteCommand2 = new Command(async () => await LoadRoute2());
            StopRouteCommand = new Command(StopRoute);
            GetPlacesCommand = new Command<string>(async (param) => await GetPlacesByName(param));

            LoadAntennas();
        }
        public GoogleMapsViewModel(string param)
        {
            _store = new RecordsStore();
            Antennas = new ObservableCollection<CompanyLocation>();
            ToggleAccelerometer();
            LoadRouteCommand = new Command(async () => await LoadRoute());
            LoadRouteCommand2 = new Command(async () => await LoadRoute2());
            StopRouteCommand = new Command(StopRoute);
            
            stopWatch.Start(); 

            Device.StartTimer(TimeSpan.FromMilliseconds(1), () =>
            {
                StopWatchHours = stopWatch.Elapsed.Hours.ToString("00");
                StopWatchMinutes = stopWatch.Elapsed.Minutes.ToString("00");
                StopWatchSeconds = stopWatch.Elapsed.Seconds.ToString("00");
                StopWatchMilliseconds = stopWatch.Elapsed.Milliseconds.ToString("00");

                if (StopWatchHours == "00" && StopWatchMinutes == "00")
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
            StartCommand = new Command(StartStopwatch);
            RestartCommand = new Command(RestartStopwatch);
            LoadAntennas();
        }

        private async void LoadAntennas()
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream($"VYRMobile.Antenas.locations.json");
            string locationsData;
            using (var reader = new System.IO.StreamReader(stream))
            {
                locationsData = reader.ReadToEnd();
            }

            var locations = JsonConvert.DeserializeObject<List<CompanyLocation>>(locationsData);

            Antennas.Clear();


            foreach (var location in locations)
            {
                Antennas.Add(location);
            }

            /*try
            {
                var antennas = await ReportsStore.Instance.GetAntenasAsync();

                Antennas.Clear();
                foreach (var antenna in antennas)
                {
                    Antennas.Add(antenna);
                }
            }
            catch
            {
                await App.Current.MainPage.DisplayAlert("Error", "Hubo un problema al procesar la información", "Aceptar");
            }*/
        }

        public async Task LoadRoute()
        {
            ActualLocationCommand.Execute(null);
            var googleDirection = await googleMapsApi.GetDirections(_originLatitud, _originLongitud, _destinationLatitud, _destinationLongitud);

            if (googleDirection.Routes != null && googleDirection.Routes.Count > 0)
            {
                var positions = (Enumerable.ToList(PolylineHelper.Decode(googleDirection.Routes.First().OverviewPolyline.Points)));
                CalculateRouteCommand.Execute(positions);

                HasRouteRunning = true;

                var record = new Record()
                {
                    UserId = await SecureStorage.GetAsync("id"),
                    Type = "Ruta Iniciada",
                    RecordType = Record.RecordTypes.RouteStarted,
                    Owner = "Yo",
                    Date = DateTime.Now,
                    Icon = "startRoute.png"
                };

                App.Records.Add(record);
                var Records = App.Records;
                var json = JsonConvert.SerializeObject(Records);
                await SecureStorage.SetAsync("records", json);

                //await _store.AddRecordAsync(record);

                //Location tracking simulation
                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    CalculateDistance();
                    if (HasRouteRunning)
                    {
                        UpdatePositionCommand.Execute(positions);
                        return true;
                    }
                    else
                    {
                        //App.Current.MainPage.DisplayAlert(":)", "Has llegado a tu destino.", "Ok");
                        ////if (positions.Count <= positionIndex && !HasRouteRunning)
                        ////{
                        ////}
                        ////else
                        ////{
                        ////    App.Current.MainPage.DisplayAlert(":(", "Tu ruta se ha cancelado, presion 'Start Route' para inicar una nueva ruta.", "Ok");
                        ////}
                           
                        return false;
                    }

                });
            }
            else
            {
                await App.Current.MainPage.DisplayAlert(":)", "No route found", "Ok");
            }
        }
        public async Task LoadRoute2()
        {
            await GetActualLocation();

            var googleDirection = await googleMapsApi.GetDirections(_originLatitud, _originLongitud,  _destinationLatitud, _destinationLongitud);

            if (googleDirection.Routes != null && googleDirection.Routes.Count > 0)
            {
                var positions = (Enumerable.ToList(PolylineHelper.Decode(googleDirection.Routes.First().OverviewPolyline.Points)));

                MapPage.Instance.CalculateCommand2.Execute(positions);

                HasRouteRunning = true;

                //Location tracking simulation
                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    CalculateDistance();
                    if (HasRouteRunning)
                    {
                        MapPage.Instance.UpdateCommand.Execute(positions);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                });
            }
            else
            {
                await App.Current.MainPage.DisplayAlert(":)", "No route found", "Ok");
            }
        }
        public async void StopRoute()
        {
            HasRouteRunning = false;

            var record = new Record()
            {
                UserId = await SecureStorage.GetAsync("id"),
                Type = "Tiempo recorrido",
                RecordType = Record.RecordTypes.LogIn,
                Owner = stopWatch.Elapsed.ToString(),
                Date = DateTime.Now,
                Icon = "chrono.png"
            };

            App.Records.Add(record);
            var Records = App.Records;
            var json = JsonConvert.SerializeObject(Records);
            await SecureStorage.SetAsync("records", json);

            stopWatch.Stop();

            //await _store.AddRecordAsync(record);
        }

        public async Task GetPlacesByName(string placeText)
        {
            var places = await googleMapsApi.GetPlaces(placeText);
            var placeResult = places.AutoCompletePlaces;
            if (placeResult != null && placeResult.Count > 0)
            {
                Places = new ObservableCollection<GooglePlaceAutoCompletePrediction>(placeResult);
            }

            ShowRecentPlaces = (placeResult == null || placeResult.Count == 0);
        }

        //public async Task GetPlacesDetail(GooglePlaceAutoCompletePrediction placeA)
        //{
        //    var place = await googleMapsApi.GetPlaceDetails(placeA.PlaceId);
        //    if (place != null)
        //    {
        //        if (_isPickupFocused)
        //        {
        //            PickupText = place.Name;
        //            _originLatitud = $"{place.Latitude}";
        //            _originLongitud = $"{place.Longitude}";
        //            _isPickupFocused = false;
        //            FocusOriginCommand.Execute(null);
        //        }
        //        else
        //        {
        //            _destinationLatitud = $"{place.Latitude}";
        //            _destinationLongitud = $"{place.Longitude}";

        //            RecentPlaces.Add(placeA);

        //            if (_originLatitud == _destinationLatitud && _originLongitud == _destinationLongitud)
        //            {
        //                await App.Current.MainPage.DisplayAlert("Error", "Origin route should be different than destination route", "Ok");
        //            }
        //            else
        //            {
        //                LoadRouteCommand.Execute(null);
        //                await App.Current.MainPage.Navigation.PopAsync(false);
        //                CleanFields();
        //            }

        //        }
        //    }
        //}

        //void CleanFields()
        //{
        //    PickupText = OriginText = string.Empty;
        //    ShowRecentPlaces = true;
        //    PlaceSelected = null;
        //}


        //Get place 
        //public async Task GetLocationName(Position position)
        //{
        //    try
        //    {
        //        var placemarks = await Geocoding.GetPlacemarksAsync(position.Latitude, position.Longitude);
        //        var placemark = placemarks?.FirstOrDefault();
        //        if (placemark != null)
        //        {
        //            PickupText = placemark.FeatureName;
        //        }
        //        else
        //        {
        //            PickupText = string.Empty;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.WriteLine(ex.ToString());
        //    }
        //}

        /*void Accelerometer_ReadingChanged(object sender, AccelerometerChangedEventArgs e)
        {
            var dataX = e.Reading.Acceleration.X.ToString();
            var dataY = e.Reading.Acceleration.Y.ToString();
            var dataZ = e.Reading.Acceleration.Z.ToString();

            AccelerometerData = $"{dataX}, {dataY}, {dataZ}";

            // Process Acceleration X, Y, and Z
        }*/

        public void ToggleAccelerometer()
        {
            try
            {
                if (Accelerometer.IsMonitoring)
                    Accelerometer.Stop();
                else
                    Accelerometer.Start(speed);
            }
            catch (FeatureNotSupportedException )
            {
                // Feature not supported on device
            }
            catch (Exception )
            {
                // Other error has occurred.
            }
        }
        private async Task CalculateDistance()
        {
            HomeViewModel puntoViewModel = new HomeViewModel();
            var myLatitude = double.Parse(this._destinationLatitud);
            var myLongitude = double.Parse(this._destinationLongitud);

            var myLoc = await Geolocation.GetLastKnownLocationAsync();
            Location rootLoc = new Location(myLoc.Latitude, myLoc.Longitude);
            Location destinationLoc = new Location(myLatitude, myLongitude);

            var distance = Location.CalculateDistance(rootLoc, destinationLoc, DistanceUnits.Kilometers);
            
            if(distance < 0.1)
            {
                puntoViewModel.StopCommand.Execute(null);
                StopRoute();
                MapPage.Instance.PolylinesCommand.Execute(null);
                await App.Current.MainPage.DisplayAlert("Ruta completa", "Has llegado a tu destino", "OK");
            }
        }
        private async Task GetActualLocation()
        {
            bool isLocationPermited = await _permissions.CheckLocationPermissionsStatus();

            if (isLocationPermited)
            {
                try
                {
                    var location = await Geolocation.GetLastKnownLocationAsync();
                    Position position = new Position(location.Latitude, location.Longitude);

                    if (location != null)
                    {
                        _originLatitud = position.Latitude.ToString();
                        _originLongitud = position.Longitude.ToString();
                        /*_destinationLatitud = App.Alarm.Location.Latitude.ToString();
                        _destinationLongitud = App.Alarm.Location.Longitude.ToString();*/
                    }
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Error", $"No es posible obtener tu ubicacion {ex.Message}", "Aceptar");
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Fallido", "Por favor activa tu GPS para continuar", "Aceptar");
            }
        }
        private async void StartStopwatch()
        {
            /* await App.Current.MainPage.Navigation.PopPopupAsync();

             await Task.Delay(100);
             IsLogo = false;
             IsChrono = true;
             IsButton = false;
             stopWatch.Start();
             ChronoColorChange(timeSpan);*/

            await App.Current.MainPage.Navigation.PopPopupAsync();

            TimeSpan timeSpan = new TimeSpan();
            timeSpan = TimeSpan.FromSeconds(60);

            await App.Current.MainPage.Navigation.PushModalAsync(new NavigationPage(new MapPage("" /*App.Alarm.LocationName, App.Alarm.Location*/)));
            //stopWatch.Start();

            //ChronoColorChange(timeSpan);
        }
        private void RestartStopwatch()
        {
            stopWatch.Restart();
        }
        public void StopStopwatch()
        {
            if (stopWatch.IsRunning == true)
            {
                stopWatch.Stop();
            }
        }
        private async void ChronoColorChange(TimeSpan timeSpan)
        {
            while (stopWatch.IsRunning == true && timeSpan > stopWatch.Elapsed)
            {
                ChronoColor = Color.Green;
                await Task.Delay(1000);
                ChronoColor = Color.Black;
                await Task.Delay(1000);
            }

            while (stopWatch.IsRunning == true && timeSpan < stopWatch.Elapsed)
            {
                ChronoColor = Color.Red;
                await Task.Delay(1000);
                ChronoColor = Color.Black;
                await Task.Delay(1000);
            }
        }
    }
}

