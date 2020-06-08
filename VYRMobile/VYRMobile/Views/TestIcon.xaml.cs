using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.Controls;
using VYRMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Xaml;

namespace VYRMobile.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TestIcon : ContentPage
    {
        bool IsFading = true;
        public TestIcon()
        {
            InitializeComponent();

            string param = "";
            BindingContext = new GoogleMapsViewModel(param);

            /*typeLbl.Text = App.Alarm.Type;

            AddPin(App.Alarm);

            AddMapStyle();
            shakeImage();*/
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Animate();
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
        /*private void AddPin(FirestoreAlarm alarm)
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
            MoveCamera(App.Alarm.Location);
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
        private async void MoveCamera(GeoPoint geoPoint)
        {
            Position myPosition = new Position(geoPoint.Latitude, geoPoint.Longitude);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(myPosition, Distance.FromMeters(500)));
        }*/
    }
}