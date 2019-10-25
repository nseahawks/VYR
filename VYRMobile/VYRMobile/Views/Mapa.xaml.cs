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

namespace VYRMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Mapa : ContentPage
    {
        public Mapa()
        {
            InitializeComponent();

            Task.Delay(2000);
            UpdateMap();
           
        }
        List<Place> placesList = new List<Place>();
        /*public void OnMapReady(GoogleMap map)
        {
            MarkerOptions markerOpt1 = new MarkerOptions();
            markerOpt1.SetPosition(new LatLng(50.379444, 2.773611));
            markerOpt1.SetTitle("Vimy Ridge");

            map.AddMarker(markerOpt1);
        }*/
        private async void UpdateMap()
        {
            try
            {
                var assembly = IntrospectionExtensions.GetTypeInfo(typeof(MainPage)).Assembly;
                Stream stream = assembly.GetManifestResourceStream("VYRMobile.Places.json");
                string text = string.Empty;
                using (var reader = new StreamReader(stream))
                {
                    text = reader.ReadToEnd();
                }

                var resultObject = JsonConvert.DeserializeObject<Places>(text);

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


                //var lat = (double.Parse(loc.Latitude.ToString()));
                //var lng = (double.Parse(loc.Longitude.ToString()));

                MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(47.6370891183, -122.123736172), Distance.FromKilometers(100)));

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }


        }
    }
}