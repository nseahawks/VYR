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
        string destinationName;
        double destinationLatitude;
        double destinationLongitude;
        public AlarmPopup(string locationName, string type, double latitude, double longitude)
        {
            InitializeComponent();

            string param = "";
            BindingContext = new GoogleMapsViewModel(param);

            typeLbl.Text = type;
            destinationName = locationName;
            destinationLatitude = latitude;
            destinationLongitude = longitude;

            AddPin(destinationName, destinationLatitude, destinationLongitude);

            AddMapStyle();
            shakeImage();
        }
        private void AddPin(string name, double latitude, double longitude)
        {

            Pin locationPin = new Pin
            {
                Type = PinType.SavedPin,
                Label = name,
                Icon = BitmapDescriptorFactory.FromBundle("mapAntenna.png"),
                Position = new Position(latitude, longitude)
            };

            map.Pins.Add(locationPin);

            map.Circles = new List<CustomCircle>
            {
                new CustomCircle{ Position = locationPin.Position, Radius = 50}
            };
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            MoveCamera(destinationLatitude, destinationLongitude);
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
        private void MoveCamera(double latitude, double longitude)
        {
            Position myPosition = new Position(latitude, longitude);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(myPosition, Distance.FromMeters(500)));
        }
    }
}