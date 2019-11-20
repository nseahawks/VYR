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
using Xamarin.Forms.Maps;
using Xamarin.Essentials;
using Plugin.Geolocator;
//using Xamarin.Forms.GoogleMaps;
using System.Windows.Input;
//using Position = Xamarin.Forms.GoogleMaps.Position;

namespace VYRMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Mapa : ContentPage
    {
        public Mapa()
        {
            InitializeComponent();

            Task.Delay(2000);
            //UpdateMap();
            AddMapStyle();

            /*Xamarin.Forms.GoogleMaps.Pin seahawksPin = null;


            seahawksPin = new Xamarin.Forms.GoogleMaps.Pin()
                {
                    Type = Xamarin.Forms.GoogleMaps.PinType.Place,
                    Label = "Negocios Seahawks",
                    Address = "Av. Roberto Pastoriza 869, Santo Domingo 10147",
                    Position = new Position(18.461294d, -69.948531d),
                    Rotation = 33.3f,
                    Tag = "id_seahawks"
                };

                MyMap.Pins.Add(seahawksPin);
                MyMap.MoveToRegion(Xamarin.Forms.GoogleMaps.MapSpan.FromCenterAndRadius(seahawksPin.Position, Xamarin.Forms.GoogleMaps.Distance.FromMeters(5000)));*/
            Pin seahawks = new Pin
            {
                Label = "Negocios Seahawks",
                Address = "Av. Roberto Pastoriza 869, Santo Domingo 10147",
                Type = PinType.Place,
                Position = new Position(18.461294d, -69.948531d)
            };
            MyMap.Pins.Add(seahawks);

            Xamarin.Forms.Maps.Polyline polyline = new Xamarin.Forms.Maps.Polyline
            {
                StrokeColor = Color.Blue,
                StrokeWidth = 12,
                Geopath =
            {
                new Position(18.461294d, -69.948531d),
                new Position(18.476740d, -69.913872d),
                new Position(18.548335d, -69.867444d)
            }
            };
            MyMap.MapElements.Add(polyline);


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

            //MyMap.MapStyle = MapStyle.FromJson(styleFile);
        }

        List<Place> placesList = new List<Place>();

        /*public void OnMapReady(GoogleMap map)
{
    MarkerOptions seahawks = new MarkerOptions();
    seahawks.SetPosition(new LatLng());
    seahawks.SetTitle("Negocios Seahawks");

    map.AddMarker(seahawks);
}*/
        private async void UpdateMap()
        {
            /*var seahawks = new Position(18.461294, -69.948531);

            var seahawksPin = new Xamarin.Forms.GoogleMaps.Pin
            {
                Type = Xamarin.Forms.GoogleMaps.PinType.Place,
                Position = seahawks,
                Label = "Negocios Seahawks",
                Address = "Av. Roberto Pastoriza 869, Santo Domingo 10147"
            };
            MyMap.Pins.Add(seahawksPin);
            MyMap.MoveToRegion(Xamarin.Forms.GoogleMaps.MapSpan.FromCenterAndRadius(seahawksPin.Position, Xamarin.Forms.GoogleMaps.Distance.FromMeters(5000)));

            ((Button)sender).IsEnabled = false;
            buttonRemoveSeahawksPin.IsEnabled = true;*/
            try
            {

                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly;
                /*Stream stream = assembly.GetManifestResourceStream("VYRMobile.Places.json");
                string text = string.Empty;
                using (var reader = new StreamReader(stream))
                {
                    text = reader.ReadToEnd();
                }

                /*var resultObject = JsonConvert.DeserializeObject<Places>(text);

                foreach (var place in resultObject.results)
                {
                    placesList.Add(new Place
                    {
                        PlaceName = place.name,
                        Address = place.vicinity,
                        Location = place.geometry.location,
                        Position = new Position(place.geometry.location.lat, place.geometry.location.lng),
                        //Icon = place.icon,
                        //Distance = $"{GetDistance(lat1, lon1, place.geometry.location.lat, place.geometry.location.lng, DistanceUnit.Kiliometers).ToString("N2")}km",
                        //OpenNow = GetOpenHours(place?.opening_hours?.open_now)
                    });
                }

                MyMap.ItemsSource = placesList;
                //PlacesListView.ItemsSource = placesList;
                //var request = new GeolocationRequest(GeolocationAccuracy.Medium);
                var loc = await Xamarin.Essentials.Geolocation.GetLocationAsync();
                */

        //var lat = (double.Parse(loc.Latitude.ToString()));
        //var lng = (double.Parse(loc.Longitude.ToString()));

        var locator = CrossGeolocator.Current;
        locator.DesiredAccuracy = 100;
        var position = await locator.GetPositionAsync();
        Xamarin.Forms.Maps.Position _position = new Xamarin.Forms.Maps.Position(position.Latitude, position.Longitude);
        MyMap.MoveToRegion(Xamarin.Forms.Maps.MapSpan.FromCenterAndRadius(new Xamarin.Forms.Maps.Position(_position.Latitude, _position.Longitude), Xamarin.Forms.Maps.Distance.FromMiles(0.2)));

    }
    catch (Exception ex)
    {
        Debug.WriteLine(ex);
    }


    }
    }
}