using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
//using Xamarin.Forms.Maps;
using Xamarin.Essentials;
using Plugin.Geolocator;
using Xamarin.Forms.GoogleMaps;
using System.Windows.Input;
using Position = Xamarin.Forms.GoogleMaps.Position;

namespace VYRMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Mapa2 : ContentPage
    {
        public Mapa2()
        {
            InitializeComponent();

            Task.Delay(2000);
            //UpdateMap();
            AddMapStyle();
            //Route.Clicked += Route_clicked();
            Route.Clicked += Route_clicked;
            stopRoute.Clicked += stopRoute_clicked;
            Xamarin.Forms.GoogleMaps.Pin seahawksPin = null;


            seahawksPin = new Xamarin.Forms.GoogleMaps.Pin()
                {
                    Type = Xamarin.Forms.GoogleMaps.PinType.Place,
                    Label = "Negocios Seahawks",
                    Address = "Av. Roberto Pastoriza 869, Santo Domingo 10147",
                    Position = new Position(18.461294d, -69.948531d),
                    Rotation = 33.3f,
                    Tag = "id_seahawks"
        };

                Mapa.Pins.Add(seahawksPin);
                Mapa.MoveToRegion(Xamarin.Forms.GoogleMaps.MapSpan.FromCenterAndRadius(seahawksPin.Position, Xamarin.Forms.GoogleMaps.Distance.FromMeters(5000)));

            
        }

        private void stopRoute_clicked(object sender, EventArgs e)
        {
            Route.IsVisible = true;
            stopRoute.IsVisible = false;
        }

        private void Route_clicked(object sender, EventArgs e)
        {
            //Position position;
            stopRoute.IsVisible = true;
            Route.IsVisible = false;
            //Mapa.Position = new CameraPosition(position.Latitude, position.Longitude);
            //Calculate();
        }
        private async void Update(Position position)
        {
            if (Mapa.Pins.Count == 1 && Mapa.Polylines != null && Mapa.Polylines?.Count > 1)
                return;

            var cPin = Mapa.Pins.FirstOrDefault();

            if (cPin != null)
            {
                cPin.Position = new Position(position.Latitude, position.Longitude);
                cPin.Icon = BitmapDescriptorFactory.FromView(new Image() { Source = "antena.png", WidthRequest = 25, HeightRequest = 25 });

                await Mapa.MoveCamera(CameraUpdateFactory.NewPosition(new Position(position.Latitude, position.Longitude)));
                var previousPosition = Mapa?.Polylines?.FirstOrDefault()?.Positions?.FirstOrDefault();
                Mapa.Polylines?.FirstOrDefault()?.Positions?.Remove(previousPosition.Value);
            }
            else
            {
                //END TRIP
                Mapa.Polylines?.FirstOrDefault()?.Positions?.Clear();
            }


        }
        void Calculate(List<Position> list)
        {
            //searchLayout.IsVisible = false;
            stopRoute.IsVisible = true;
            Mapa.Polylines.Clear();
            var polyline = new Polyline();
            foreach (var p in list)
            {
                polyline.Positions.Add(p);

            }
            Mapa.Polylines.Add(polyline);
            Mapa.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(polyline.Positions[0].Latitude, polyline.Positions[0].Longitude), Xamarin.Forms.GoogleMaps.Distance.FromMiles(0.50f)));

            var pin = new Pin
            {
                Type = PinType.Place,
                Position = new Position(polyline.Positions.First().Latitude, polyline.Positions.First().Longitude),
                Label = "First",
                Address = "First",
                Tag = string.Empty,
                Icon = BitmapDescriptorFactory.FromView(new Image() { Source = "antena.png", WidthRequest = 25, HeightRequest = 25 })

            };
            Mapa.Pins.Add(pin);
            var pin1 = new Pin
            {
                Type = PinType.Place,
                Position = new Position(polyline.Positions.Last().Latitude, polyline.Positions.Last().Longitude),
                Label = "Last",
                Address = "Last",
                Tag = string.Empty
            };
            Mapa.Pins.Add(pin1);
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

            Mapa.MapStyle = MapStyle.FromJson(styleFile);
        }

        List<Place> placesList = new List<Place>();

        private async void UpdateMap()
        {
            try
            {

                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly;
               

                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 100;
                var position = await locator.GetPositionAsync();
                Xamarin.Forms.Maps.Position _position = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
                Mapa.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(_position.Latitude, _position.Longitude), Distance.FromMiles(0.2)));

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
        }
}