using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.Controls;
using VYRMobile.Models;
using VYRMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlarmPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        bool IsFading = true;
        GeoPoint destination;
        public AlarmPopup(FirestoreAlarm alarm)
        {
            InitializeComponent();

            string param = "";
            BindingContext = new GoogleMapsViewModel(param);

            typeLbl.Text = alarm.Type;
            destination = alarm.Location;

            AddPin(alarm);

            AddMapStyle();
            shakeImage();
        }
        private void AddPin(FirestoreAlarm alarm)
        {

            Pin locationPin = new Pin
            {
                Type = PinType.SavedPin,
                Label = alarm.LocationName,
                Icon = BitmapDescriptorFactory.FromBundle("mapAntenna.png"),
                Position = new Position(alarm.Location.Latitude, alarm.Location.Longitude)
            };

            map.Pins.Add(locationPin);

            map.Circle = new CustomCircle
            {
                Position = locationPin.Position,
                Radius = 50
            };
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            MoveCamera(destination);
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
        private async void shakeImage()
        {
            while (IsFading == true)
            {
                await gradient.FadeTo(0.3, 500, Easing.Linear);
                await gradient.FadeTo(0.8, 500, Easing.Linear);
                await gradient.FadeTo(0.3, 500, Easing.Linear);
                await gradient.FadeTo(0.8, 500, Easing.Linear);
            }
        }
        private void MoveCamera(GeoPoint geoPoint)
        {
            Position myPosition = new Position(geoPoint.Latitude, geoPoint.Longitude);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(myPosition, Distance.FromMeters(500)));
        }
    }
}