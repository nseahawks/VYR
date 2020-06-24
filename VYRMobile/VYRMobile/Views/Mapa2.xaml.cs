using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using VYRMobile.Controls;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Position = Xamarin.Forms.GoogleMaps.Position;
using Xamarin.Essentials;
using VYRMobile.ViewModels;
using VYRMobile.Helper;
using Location = Xamarin.Essentials.Location;
using VYRMobile.Data;
using Plugin.CloudFirestore;
using VYRMobile.Services;
using Plugin.Geolocator;

namespace VYRMobile.Views
{
    public partial class Mapa2 : ContentPage
    {
        bool IsFading = true;
        bool AlarmMode = false;
        IGoogleMapsApiService googleMapsApi = new GoogleMapsApiService();
        SensorSpeed speed = SensorSpeed.Default;
        private double CData;
        private double TData;
        private double RouteDistance;
        LineHelper liner = new LineHelper();
        public Command CalculateCommand2 { get; set; }
        public Command PolylinesCommand { get; set; }

        public static readonly BindableProperty CalculateCommandProperty =
           BindableProperty.Create(nameof(CalculateCommand), typeof(Command), typeof(Mapa2), null, BindingMode.TwoWay);
        
        public static readonly BindableProperty IsRouteRunningProperty =
           BindableProperty.Create(nameof(IsRouteRunning), typeof(bool), typeof(Mapa2), null, BindingMode.TwoWay);

        public static readonly BindableProperty GetActualLocationCommandProperty =
            BindableProperty.Create(nameof(GetActualLocationCommand), typeof(Command),
                typeof(Mapa2), null, BindingMode.TwoWay);

        public static readonly BindableProperty OriginLocationlatProperty =
           BindableProperty.Create(nameof(OriginLocationlat), typeof(string),
               typeof(Mapa2), null, BindingMode.TwoWay);

        public static readonly BindableProperty OriginLocationlngProperty =
        BindableProperty.Create(nameof(OriginLocationlng), typeof(string),
            typeof(Mapa2), null, BindingMode.TwoWay);
        
        public static readonly BindableProperty DestinationLocationlatProperty =
        BindableProperty.Create(nameof(DestinationLocationlat), typeof(string),
            typeof(Mapa2), null, BindingMode.TwoWay);
        
        public static readonly BindableProperty DestinationLocationlngProperty =
        BindableProperty.Create(nameof(DestinationLocationlng), typeof(string),
            typeof(Mapa2), null, BindingMode.TwoWay);
   
        public static readonly BindableProperty UpdateCommandProperty =
          BindableProperty.Create(nameof(UpdateCommand), typeof(Command), typeof(Mapa2), null, BindingMode.TwoWay);

        public Command UpdateCommand
        {
            get { return (Command)GetValue(UpdateCommandProperty); }
            set { SetValue(UpdateCommandProperty, value); }
        }

        private static Mapa2 _instance;
        public static Mapa2 Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Mapa2();

                return _instance;
            }
        }

        public Mapa2(string locationName, GeoPoint geoPoint)
        {
            InitializeComponent();
            string param = "";
            BindingContext = new GoogleMapsViewModel(param);
            AddMapStyle();

            comboBox.IsVisible = false;
            startRoute.IsVisible = false;
            //animation.IsVisible = true;
            AlarmMode = true;
            DestinationLocationlat = geoPoint.Latitude.ToString();
            DestinationLocationlng = geoPoint.Longitude.ToString();

            CalculateCommand = new Command<List<Position>>(Calculate);
            CalculateCommand2 = new Command<List<Position>>(Calculate2);
            UpdateCommand = new Command<List<Position>>(Update);
            PolylinesCommand = new Command(ClearPolylinesCommand);
            GetActualLocationCommand = new Command(async () => await GetActualLocation());

            //alarmMode();
            Pin destinationPin;
            destinationPin = new Pin()
            {
                Type = PinType.SavedPin,
                Icon = BitmapDescriptorFactory.FromBundle("mapAntenna.png"),
                Label = locationName,
                Position = new Position(geoPoint.Latitude, geoPoint.Longitude)
            };

            map.Circle = new CustomCircle
            {
                Position = destinationPin.Position,
                Radius = 50
            };

            map.Pins.Add(destinationPin);

            Compass.ReadingChanged += Compass_ReadingChanged;
            OrientationSensor.ReadingChanged += OrientationSensor_ReadingChanged;
            map.MapClicked += Map_MapClicked;
            LoadRoute2();

            //await();
        }
        private async void await()
        {
            await Task.Delay(2000);
        }
        private void Map_MapClicked(object sender, MapClickedEventArgs e)
        {
            if(chronoFrame.Opacity == 1)
            {
                uint duration = 500;
                var animation = new Animation();
                var animation2 = new Animation();

                animation.WithConcurrent(
                  (f) => chronoFrame.TranslationY = f,
                  chronoFrame.TranslationY - 0, -  10,
                  Easing.CubicOut, 1, 0);

                animation.WithConcurrent((f) => chronoFrame.Opacity = f, 1, 0, Easing.CubicIn);

                animation2.WithConcurrent(
                  (f) => estimatedTimeFrame.TranslationY = f,
                  estimatedTimeFrame.TranslationY + 0, + 10,
                  Easing.CubicOut, 1, 0);

                animation2.WithConcurrent((f) => estimatedTimeFrame.Opacity = f, 1, 0, Easing.CubicIn);

                chronoFrame.Animate("FadeIn", animation, 16, Convert.ToUInt32(duration));
                estimatedTimeFrame.Animate("FadeIn", animation2, 16, Convert.ToUInt32(duration));
            }
            else
            {
                ShowChrono();
            }
        }

        public Mapa2()
        {
            InitializeComponent();
            BindingContext = new GoogleMapsViewModel();
            chronoFrame.IsVisible = false;
            chronoFrame.IsEnabled = false;
            estimatedTimeFrame.IsVisible = false;
            estimatedTimeFrame.IsEnabled = false;
            AddMapStyle();
            AddLocations();
            comboBox.SelectionChanged += AntennaSelected;

            CalculateCommand = new Command<List<Position>>(Calculate);
            CalculateCommand2 = new Command<List<Position>>(Calculate2);
            UpdateCommand = new Command<List<Position>>(Update);
            PolylinesCommand = new Command(ClearPolylinesCommand);
            GetActualLocationCommand = new Command(async () => await GetActualLocation());
            startRoute.IsEnabled = false;

            Pin seahawksPin = null;
            seahawksPin = new Pin()
            {
                Type = PinType.SavedPin,
                Icon = BitmapDescriptorFactory.FromBundle("mapAntenna.png"),
                Label = "Negocios Seahawks",
                Address = "Av. Roberto Pastoriza 869, Santo Domingo 10147",
                Position = new Position(18.461294, -69.948531),
                Tag = "id_seahawks"
            };

            map.Circle = new CustomCircle
            {
                Position = seahawksPin.Position,
                Radius = 50
            };


            map.Pins.Add(seahawksPin);

            //Compass.ReadingChanged += Compass_ReadingChanged;
            ///OrientationSensor.ReadingChanged += OrientationSensor_ReadingChanged;
            map.PinClicked += Map_PinClicked;
        }
        
        private async void AddLocations()
        {
            var antennas = await ReportsStore.Instance.GetAntenasAsync();

            foreach (var antenna in antennas)
            {
                Pin antennaPin = new Pin
                {
                    Type = PinType.SavedPin,
                    Label = antenna.LocationName,
                    Icon = BitmapDescriptorFactory.FromBundle("mapAntenna.png"),
                    Address = antenna.Address,
                    Position = new Position(antenna.Latitude, antenna.Longitude),
                    Tag = antenna.Id
                };

                map.Pins.Add(antennaPin);
            }
        }
        private async void ClearPolylines(Object sender, EventArgs e)
        {
            map.Polylines.Clear();
            await DisplayAlert("Ruta detenida", "Has cancelado el progreso de la ruta", "OK");
        }
        private void ClearPolylinesCommand()
        {
            map.Polylines.Clear();
        }
        private async void AntennaSelected(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            var ind = comboBox.SelectedIndex;
            var pin = map.Pins.ElementAt<Pin>(ind);

            DestinationLocationlat = pin.Position.Latitude.ToString();
            DestinationLocationlng = pin.Position.Longitude.ToString();

            await map.AnimateCamera(CameraUpdateFactory.NewCameraPosition(new CameraPosition
                (new Position(pin.Position.Latitude, pin.Position.Longitude),12d,0,0)));
            startRoute.IsEnabled = true;
        }
        private void OrientationSensor_ReadingChanged(object sender, OrientationSensorChangedEventArgs e)
        {
            var data = e.Reading;
            var angle = (2 * Math.Acos(data.Orientation.W))* 60;

            if(angle >= 90.0)
            {
                TData = 90.0;
            } 
            else if (angle <= 0)
            {
                TData = 0;
            }
            else
            {
                TData = angle;
            }
        }

        void Compass_ReadingChanged(object sender, CompassChangedEventArgs e)
        {
            var data = e.Reading;
            CData = data.HeadingMagneticNorth;
        }

        public void ToggleCompass()
        {
            try
            {
                if (Compass.IsMonitoring)
                    Compass.Stop();
                else
                    Compass.Start(speed, applyLowPassFilter: true);
            }
            catch (FeatureNotSupportedException )
            {
                // Feature not supported on device
            }
            catch (Exception )
            {
                // Some other exception has occurred
            }
        }

        public void ToggleOrientation()
        {
            try
            {
                if (OrientationSensor.IsMonitoring)
                    OrientationSensor.Stop();
                else
                    OrientationSensor.Start(speed);
            }
            catch (FeatureNotSupportedException )
            {
                // Feature not supported on device
            }
            catch (Exception )
            {
                // Some other exception has occurred
            }
        }
        void Map_PinClicked(object sender, PinClickedEventArgs e)
        {
            DestinationLocationlat = e.Pin.Position.Latitude.ToString();
            DestinationLocationlng = e.Pin.Position.Longitude.ToString();

            if(DestinationLocationlat != null && DestinationLocationlng != null)
            {
                startRoute.IsEnabled = true;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            map.MyLocationEnabled = true;
            map.UiSettings.MyLocationButtonEnabled = true;
            map.UiSettings.TiltGesturesEnabled = true;
            ToggleCompass();
            ToggleOrientation();
            GetActualLocationCommand.Execute(null);
            MoveCamera();
            ShowChrono();
            Animate();
        }

        private async void MoveCamera()
        {
            //var request = new GeolocationRequest(GeolocationAccuracy.High);
            var location = await Geolocation.GetLastKnownLocationAsync();
            Position myPosition = new Position(location.Latitude, location.Longitude);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(myPosition, Distance.FromMeters(1000)));
        }
        public Command CalculateCommand
        {
            get { return (Command)GetValue(CalculateCommandProperty); }
            set { SetValue(CalculateCommandProperty, value); }
        }

        public string OriginLocationlat
        {
            get { return (string)GetValue(OriginLocationlatProperty); }
            set { SetValue(OriginLocationlatProperty, value); }
        }
        
        public bool IsRouteRunning
        {
            get { return (bool)GetValue(IsRouteRunningProperty); }
            set { SetValue(IsRouteRunningProperty, value); }
        }
        public string OriginLocationlng
        {
            get { return (string)GetValue(OriginLocationlngProperty); }
            set { SetValue(OriginLocationlngProperty, value); }
        }
        public string DestinationLocationlat
        {
            get { return (string)GetValue(DestinationLocationlatProperty); }
            set { SetValue(DestinationLocationlatProperty, value); }
        }
        public string DestinationLocationlng
        {
            get { return (string)GetValue(DestinationLocationlngProperty); }
            set { SetValue(DestinationLocationlngProperty, value); }
        }

        public Command GetActualLocationCommand
        {
            get { return (Command)GetValue(GetActualLocationCommandProperty); }
            set { SetValue(GetActualLocationCommandProperty, value); }
        }

        async Task GetActualLocation()
        {
            try
            { 
                var location = await Geolocation.GetLastKnownLocationAsync();
                Position position = new Position(location.Latitude, location.Longitude );

                if (App.Alarm.Location != null)
                {
                    OriginLocationlat = position.Latitude.ToString();
                    OriginLocationlng = position.Longitude.ToString();
                    DestinationLocationlat = App.Alarm.Location.Latitude.ToString();
                    DestinationLocationlng = App.Alarm.Location.Longitude.ToString();
                }
                else if(location != null)
                {
                    OriginLocationlat = position.Latitude.ToString();
                    OriginLocationlng = position.Longitude.ToString();
                }
            }

            catch (Exception ex)
            {
                //nothing
            }
        }

        private async void Update(List<Position> list)
        {
            if (map.Polylines == null && map.Polylines?.Count == 0)
                return;
         
            if (map.Polylines.Count >= 1 || list.Count == 0)
            {
                GetActualLocationCommand.Execute(null);
                
                RouteDistance = 99999999999999999;
                var RouteIndex = -1;
                var minIntersection = new Location(0,0); 
                for(var i = 0; i < list.Count - 1; i++)
                {
                   var intersection = liner.Intersection(double.Parse(OriginLocationlat), double.Parse(OriginLocationlng),
                   list[i].Latitude, list[i].Longitude,
                   list[i+1].Latitude, list[i+1].Longitude);
                   if (liner.OutOfBound(intersection, list[i].Latitude, list[i].Longitude,
                   list[i + 1].Latitude, list[i + 1].Longitude))
                   {
                        if(
                        liner.Distance(new Location(
                            double.Parse(OriginLocationlat), 
                            double.Parse(OriginLocationlng)),
                            new Location(list[i].Latitude, 
                            list[i].Longitude)) <
                        liner.Distance(new Location(
                            double.Parse(OriginLocationlat), 
                            double.Parse(OriginLocationlng)),
                            new Location(list[i+1].Latitude,
                            list[i+1].Longitude))){
                            intersection = new Location(list[i].Latitude, list[i].Longitude);
                        }
                        else
                        {
                            intersection = new Location(list[i + 1].Latitude, list[i + 1].Longitude);
                        }
                   }
                    var distance = liner.Distance(new Location(
                              double.Parse(OriginLocationlat),
                              double.Parse(OriginLocationlng)),
                              intersection);
                   if ( distance <= 250 && distance <= RouteDistance)
                   {
                        RouteDistance = distance;
                        RouteIndex = i;
                        minIntersection = intersection;
                   }
                }
                if(RouteIndex == -1)
                {
                    //recalcular
                    map.Polylines.Clear();
                    map.Polylines?.FirstOrDefault()?.Positions?.Clear();
                    IsRouteRunning = false;
                    startRoute.Command.Execute(null);
                    return;
                }
                else
                {
                    for(var i=0; i <= RouteIndex; i++)
                    {
                        var previousPosition = map?.Polylines?.FirstOrDefault()?.Positions?.FirstOrDefault();
                        map?.Polylines?.FirstOrDefault()?.Positions?.Remove(previousPosition.Value);
                        list.RemoveAt(0);
                    }
                    map?.Polylines?.FirstOrDefault()?.Positions?.Insert(0,
                            new Position(
                               double.Parse(minIntersection.Latitude.ToString()),
                               double.Parse(minIntersection.Longitude.ToString())
                               ));
                    list.Insert(0,new Position( minIntersection.Latitude, minIntersection.Longitude));
                }

                /*await map.AnimateCamera(CameraUpdateFactory.NewCameraPosition(new CameraPosition(
                    new Position(double.Parse(OriginLocationlat), double.Parse(OriginLocationlng)),18d,CData,TData)));*/
            }
            else
            {
                //END TRIP
                map.Polylines.Clear();
                map.Polylines?.FirstOrDefault()?.Positions?.Clear();
                IsRouteRunning = false;

                startRoute.Command.Execute(null);
                return;
            }
        }

        async void Calculate(List<Position> list)
        {
            map.Polylines.Clear();
            var polyline = new Polyline() {
                StrokeWidth = 12,
                StrokeColor = Color.Orange
            };
            foreach (var p in list)
            {
                polyline.Positions.Add(p);
            }
            map.Polylines.Add(polyline);
            await map.AnimateCamera(CameraUpdateFactory.NewCameraPosition(new CameraPosition(
                new Position(
                    polyline.Positions[0].Latitude,
                    polyline.Positions[0].Longitude),
                    18d,
                    CData,
                    TData
                )));
        }
        async void Calculate2(List<Position> list)
        {

            map.Polylines.Clear();
            var polyline = new Polyline()
            {
                StrokeWidth = 12,
                StrokeColor = Color.Orange
            };
            foreach (var p in list)
            {
                polyline.Positions.Add(p);
            }
            map.Polylines.Add(polyline);

            MoveCamera();
        }
        void AddMapStyle()
        {
            var assembly = typeof(MainPage).GetTypeInfo().Assembly;
            var stream = assembly.GetManifestResourceStream($"VYRMobile.MapStyle.json");
            string styleFile;
            using (var reader = new System.IO.StreamReader(stream))
            {   
                styleFile = reader.ReadToEnd();
            }

            map.MapStyle = MapStyle.FromJson(styleFile);
        }
        private async void alarmMode()
        {
            await Task.Delay(2500);
            while (AlarmMode == true)
            {
                await animation.FadeTo(0.3, 500, Easing.Linear);
                await animation.FadeTo(0.8, 500, Easing.Linear);
                await animation.FadeTo(0.3, 500, Easing.Linear);
                await animation.FadeTo(0.8, 500, Easing.Linear);
                await animation.FadeTo(0.3, 500, Easing.Linear);
                await animation.FadeTo(0.8, 500, Easing.Linear);
                await animation.FadeTo(0, 500, Easing.Linear);
                animation.IsVisible = false;
                animation.IsEnabled = false;
                AlarmMode = false;
            }
        }
        public async Task LoadRoute2()
        {
            await GetActualLocation();

            var googleDirection = await googleMapsApi.GetDirections(OriginLocationlat, OriginLocationlng, DestinationLocationlat, DestinationLocationlng);

            if (googleDirection.Routes != null && googleDirection.Routes.Count > 0)
            {
                var positions = (Enumerable.ToList(PolylineHelper.Decode(googleDirection.Routes.First().OverviewPolyline.Points)));

                Calculate2(positions);

                IsRouteRunning = true;

                //Location tracking simulation
                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    CalculateDistance();
                    if (IsRouteRunning)
                    {
                        Update(positions);
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
        private async Task CalculateDistance()
        {
            var myLatitude = double.Parse(DestinationLocationlat);
            var myLongitude = double.Parse(DestinationLocationlng);

            var myLoc = await Geolocation.GetLastKnownLocationAsync();
            Location rootLoc = new Location(myLoc.Latitude, myLoc.Longitude);
            Location destinationLoc = new Location(myLatitude, myLongitude);

            var distance = Location.CalculateDistance(rootLoc, destinationLoc, DistanceUnits.Kilometers);

            if (distance < 0.5)
            {
                if(AlarmMode == true)
                {
                    GoogleMapsViewModel.Instance.StopCommand.Execute(null);
                    App.Alarm = null;
                }
                StopRoute();
                ClearPolylinesCommand();
                await DisplayAlert("Ruta completa", "Has llegado a tu destino", "OK");
                await Navigation.PopModalAsync();
            }
        }
        public void StopRoute()
        {
            IsRouteRunning = false;
        }
        private async void ShowChrono()
        {
            uint duration = 750;
            var animation = new Animation();
            var animation2 = new Animation();

            animation.WithConcurrent((f) => chronoFrame.Opacity = f, 0, 1, Easing.CubicOut);

            animation.WithConcurrent(
              (f) => chronoFrame.TranslationY = f,
              chronoFrame.TranslationY - 50, - 10,
              Easing.CubicOut, 0, 1);

            animation2.WithConcurrent((f) => estimatedTimeFrame.Opacity = f, 0, 1, Easing.CubicOut);

            animation2.WithConcurrent(
              (f) => estimatedTimeFrame.TranslationY = f,
              estimatedTimeFrame.TranslationY  + 50, + 10,
              Easing.CubicOut, 0, 1);

            chronoFrame.Animate("FadeIn", animation, 16, Convert.ToUInt32(duration));
            estimatedTimeFrame.Animate("FadeIn", animation2, 16, Convert.ToUInt32(duration));

        }
        private async void Animate()
        {
            Animation a = new Animation();
            while (IsFading == true)
            {
                a.Add(0, 1, new Animation(f => this.startColor.Opacity = f, 1, 0, Easing.Linear, null));
                a.Add(0, 1, new Animation(f => this.endColor.Opacity = f, 0, 1, Easing.Linear, null));
                a.Commit(
                    owner: this.startColor,
                    name: "DoubleFader",
                    length: 1000,
                    finished: (x, y) =>
                    {
                        this.startColor.FadeTo(1, 1000, Easing.CubicIn);
                    });

                await Task.Delay(1000);

                a.Add(0, 1, new Animation(f => this.startColor.Opacity = f, 0, 1, Easing.Linear, null));
                a.Add(0, 1, new Animation(f => this.endColor.Opacity = f, 1, 0, Easing.Linear, null));
                a.Commit(
                    owner: this.startColor,
                    name: "DoubleFader",
                    length: 1000,
                    finished: (x, y) =>
                    {
                        this.startColor.FadeTo(0, 1000, Easing.CubicIn);
                    });

                await Task.Delay(1000);
            }

        }
    }
}