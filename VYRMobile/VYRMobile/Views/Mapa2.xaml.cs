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
using VYRMobile.Models;

namespace VYRMobile.Views
{
    public partial class Mapa2 : ContentPage
    {
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

        public Mapa2()
        {
            InitializeComponent();
            BindingContext = new GoogleMapsViewModel();
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
                Tag = "id_seahawks",
            };
            map.Pins.Add(seahawksPin);

            
            
            Compass.ReadingChanged += Compass_ReadingChanged;
            OrientationSensor.ReadingChanged += OrientationSensor_ReadingChanged;
            map.PinClicked += Map_PinClicked;
            
            //map.PinClicked += (object s, SelectedPinChangedEventArgs e) =>
            //{
            //    string pinName = s.;
            //    DisplayAlert("Pin Clicked", $"{pinName} was clicked.", "Ok");
            //};
            //map.PinClicked += async (e, args) =>
            //{
            //    string pinName = ((Pin)e).Label;
            //    await DisplayAlert("Pin Clicked", $"{pinName} was clicked.", "Ok");
            //};

            
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

            Location prueba = new Location();
            prueba = await Geolocation.GetLocationAsync();
            var velocidad = prueba.Speed;
        }
        private void OrientationSensor_ReadingChanged(object sender, OrientationSensorChangedEventArgs e)
        {
            var data = e.Reading;
            //Console.WriteLine($"Reading: {data.HeadingMagneticNorth} degrees");
            var angle = (2 * Math.Acos(data.Orientation.W))* 60;
            if(angle >= 90.0)
            {
                TData = 90.0;
            } else if (angle <= 0)
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
            //Console.WriteLine($"Reading: {data.HeadingMagneticNorth} degrees");
            CData = data.HeadingMagneticNorth;
            // Process Heading Magnetic North
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
            //e.Handled = switchHandlePinClicked.IsToggled;
            //string pinName = e.Pin.Label;
            //DisplayAlert("Pin Clicked", $"{pinName} was clicked.", "Ok");
            DestinationLocationlat = e.Pin.Position.Latitude.ToString();
            DestinationLocationlng = e.Pin.Position.Longitude.ToString();

            if(DestinationLocationlat != null && DestinationLocationlng != null)
            {
                startRoute.IsEnabled = true;
            }
           
            //startRoute.IsEnabled = true
            // If you set e.Handled = true,
            // then Pin selection doesn't work automatically.
            // All pin selection operations are delegated to you.
            // Sample codes are below.
            //if (switchHandlePinClicked.IsToggled)
            //{
            //    map.SelectedPin = e.Pin;
            //    map.AnimateCamera(CameraUpdateFactory.NewPosition(e.Pin.Position));
            //}
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
        }

        private async void MoveCamera()
        {
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
                //var request = new GeolocationRequest(GeolocationAccuracy.Best);
                var location = await Geolocation.GetLastKnownLocationAsync();
                Position position = new Position(location.Latitude, location.Longitude );

                if (location != null)
                {
                    //map.MoveToRegion(MapSpan.FromCenterAndRadius(
                    //    position,
                    //    Distance.FromMiles(0.3)));
                    //await map.MoveCamera(CameraUpdateFactory.NewPosition(new Position(location.Latitude, location.Longitude)));
                    //await map.MoveCamera(CameraUpdateFactory.NewCameraPosition(new CameraPosition(
                    //new Position(
                    //    position.Latitude,
                    //    position.Longitude),
                    //    19d,
                    //    CData,
                    //    65d
                    //)));
                    OriginLocationlat = position.Latitude.ToString();
                    OriginLocationlng = position.Longitude.ToString();
                }
            }

            catch (Exception ex)
            {
                await DisplayAlert("Error", $"No es posible obtener tu ubicacion {ex.Message}", "Ok");
            }
          
        }

        private async void Update(List<Position> list)
        {
            //var k = 0;
            if (this.map.Polylines == null && this.map.Polylines?.Count == 0)
                return;
              
            //var cPin = map.Pins.FirstOrDefault();
         
            if (this.map.Polylines.Count >= 1 || list.Count == 0)
            {
                GetActualLocationCommand.Execute(null);
                //k = 0;
                //while (list[k] != null && OriginLocationlat == list[k].Latitude.ToString() && OriginLocationlng == list[k].Longitude.ToString())
                //{

                //    k++;

                //    if (k >= list.Count)
                //    {
                //        map.Polylines.Clear();
                //        map.Polylines?.FirstOrDefault()?.Positions?.Clear();
                //        IsRouteRunning = false;
                //        return;
                //    }
                //}
                //OriginLocationlat = list[k].Latitude.ToString();
                //OriginLocationlng = list[k].Longitude.ToString();


                //var pin = new Pin()
                //{
                //    Type = PinType.Place,
                //    Position = new Position(double.Parse(OriginLocationlat), double.Parse(OriginLocationlng)),
                //    Label = "First",
                //    Address = "First",
                //    Tag = string.Empty

                //};
                //map.Pins.Add(pin);
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
                    this.map.Polylines.Clear();
                    this.map.Polylines?.FirstOrDefault()?.Positions?.Clear();
                    IsRouteRunning = false;
                    startRoute.Command.Execute(null);
                    return;
                }
                else
                {
                    for(var i=0; i <= RouteIndex; i++)
                    {
                        var previousPosition = this.map?.Polylines?.FirstOrDefault()?.Positions?.FirstOrDefault();
                        this.map?.Polylines?.FirstOrDefault()?.Positions?.Remove(previousPosition.Value);
                        list.RemoveAt(0);
                    }
                    this.map?.Polylines?.FirstOrDefault()?.Positions?.Insert(0,
                            new Position(
                               double.Parse(minIntersection.Latitude.ToString()),
                               double.Parse(minIntersection.Longitude.ToString())
                               ));
                    list.Insert(0,new Position( minIntersection.Latitude, minIntersection.Longitude));
                }
                //cPin.Position = new Position(position.Latitude, position.Longitude);
                //await map.MoveCamera(CameraUpdateFactory.NewPosition(new Position(position.Latitude, position.Longitude)));

                //MoveCamera();
                
                await map.AnimateCamera(CameraUpdateFactory.NewCameraPosition(new CameraPosition(
                    new Position(double.Parse(OriginLocationlat), double.Parse(OriginLocationlng)),18d,CData,TData)));
                //await map.MoveCamera(CameraUpdateFactory.NewCameraPosition(new CameraPosition(
                //new Position(
                //    position.Latitude,
                //     position.Longitude),
                //    18d,
                //    0d,
                //    75d
                //)));
                //var previousPosition = map?.Polylines?.FirstOrDefault()?.Positions?.FirstOrDefault();
                //map?.Polylines?.FirstOrDefault()?.Positions?.Insert(1,
                //    new Position(
                //       double.Parse(OriginLocationlat),
                //       double.Parse(OriginLocationlng)
                //       ));
                //map.Polylines?.FirstOrDefault()?.Positions?.Remove(previousPosition.Value);
            }
            else
            {
                //END TRIP
                this.map.Polylines.Clear();
                this.map.Polylines?.FirstOrDefault()?.Positions?.Clear();
                IsRouteRunning = false;

                startRoute.Command.Execute(null);
                return;
            }
        }

        async void Calculate(List<Position> list)
        {
            //searchLayout.IsVisible = false;

            map.Polylines.Clear();
            var polyline = new Xamarin.Forms.GoogleMaps.Polyline() {
                StrokeWidth = 12,
                StrokeColor = Color.Orange
            };
            foreach (var p in list)
            {
                polyline.Positions.Add(p);
            }
            map.Polylines.Add(polyline);
            //map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(polyline.Positions[0].Latitude, polyline.Positions[0].Longitude), Distance.FromMiles(0.50f)));
            await map.AnimateCamera(CameraUpdateFactory.NewCameraPosition(new CameraPosition(
                new Position(
                    polyline.Positions[0].Latitude,
                    polyline.Positions[0].Longitude),
                    18d,
                    CData,
                    TData
                )));
            

            //var pin = new Pin
            //{
            //    Type = PinType.Place,
            //    Position = new Position(polyline.Positions.First().Latitude, polyline.Positions.First().Longitude),
            //    Label = "First",
            //    Address = "First",
            //    Tag = string.Empty

            //};
            //map.Pins.Add(pin);
            //var pin1 = new Pin
            //{
            //    Type = PinType.Place,
            //    Position = new Position(polyline.Positions.Last().Latitude, polyline.Positions.Last().Longitude),
            //    Label = "Last",
            //    Address = "Last",
            //    Tag = string.Empty,
                
            //};
            //map.Pins.Add(pin1);
        }
        async void Calculate2(List<Position> list)
        {
            //searchLayout.IsVisible = false;

            this.map.Polylines.Clear();
            var polyline = new Xamarin.Forms.GoogleMaps.Polyline()
            {
                StrokeWidth = 12,
                StrokeColor = Color.Orange
            };
            foreach (var p in list)
            {
                polyline.Positions.Add(p);
            }
            this.map.Polylines.Add(polyline);
            //TimeSpan span = TimeSpan.FromSeconds(0);
            //map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(polyline.Positions[0].Latitude, polyline.Positions[0].Longitude), Distance.FromMiles(0.50f)));
            /*await map.AnimateCamera(CameraUpdateFactory.NewCameraPosition(new CameraPosition(
                new Position(polyline.Positions[0].Latitude,polyline.Positions[0].Longitude), 18d, CData, TData)), span)*/
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


        //public async void OnEnterAddressTapped(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new SearchPlacePage() { BindingContext = this.BindingContext }, false);
        //}

    }
}