﻿using System;
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

namespace VYRMobile.Views
{
    public partial class Mapa2 : ContentPage
    {

        public static readonly BindableProperty CalculateCommandProperty =
           BindableProperty.Create(nameof(CalculateCommand), typeof(Command), typeof(Mapa2), null, BindingMode.TwoWay);

        public static readonly BindableProperty GetActualLocationCommandProperty =
            BindableProperty.Create(nameof(GetActualLocationCommand), typeof(Command),
                typeof(Mapa2), null, BindingMode.TwoWay);

        public static readonly BindableProperty OriginLocationlatProperty =
           BindableProperty.Create(nameof(OriginLocationlat), typeof(string),
               typeof(Mapa2), null, BindingMode.TwoWay);

        public static readonly BindableProperty OriginLocationlngProperty =
        BindableProperty.Create(nameof(OriginLocationlng), typeof(string),
            typeof(Mapa2), null, BindingMode.TwoWay);
   
        public static readonly BindableProperty UpdateCommandProperty =
          BindableProperty.Create(nameof(UpdateCommand), typeof(Command), typeof(Mapa2), null, BindingMode.TwoWay);

        public Command UpdateCommand
        {
            get { return (Command)GetValue(UpdateCommandProperty); }
            set { SetValue(UpdateCommandProperty, value); }
        }


        public Mapa2()
        {
            InitializeComponent();
            BindingContext = new GoogleMapsViewModel();
            AddMapStyle();
           
            CalculateCommand = new Command<List<Position>>(Calculate);
            UpdateCommand = new Command<Position>(Update);
            GetActualLocationCommand = new Command(async () => await GetActualLocation());

            //Pin seahawksPin = null;
            //seahawksPin = new Pin()
            //{
            //    Type = PinType.SavedPin,
            //    Label = "Negocios Seahawks",
            //    Address = "Av. Roberto Pastoriza 869, Santo Domingo 10147",
            //    Position = new Position(18.461294, -69.948531),
            //    Tag = "id_seahawks",
            //};
            //map.Pins.Add(seahawksPin);
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(18.461294, -69.948531), Distance.FromMeters(5000)));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            GetActualLocationCommand.Execute(null);
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

      
        public string OriginLocationlng
        {
            get { return (string)GetValue(OriginLocationlngProperty); }
            set { SetValue(OriginLocationlngProperty, value); }
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
                var request = new GeolocationRequest(GeolocationAccuracy.High);
                var location = await Geolocation.GetLocationAsync(request);
                Position position = new Position(location.Latitude, location.Longitude);

                if (location != null)
                {
                    //map.MoveToRegion(MapSpan.FromCenterAndRadius(
                    //    position,
                    //    Distance.FromMiles(0.3)));
                    await map.MoveCamera(CameraUpdateFactory.NewPosition(new Position(position.Latitude, position.Longitude)));
                    OriginLocationlat = position.Latitude.ToString();
                     OriginLocationlng = position.Longitude.ToString();
                }
            }

            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Unable to get location {ex}", "Ok");
            }
          
        }

        private async void Update(Position position)
        {
            if (map.Pins.Count == 0 && map.Polylines != null && map.Polylines?.Count > 1)
                return;

            var cPin = map.Pins.FirstOrDefault();

            if (cPin != null)
            {
                GetActualLocationCommand.Execute(null);
                //cPin.Position = new Position(position.Latitude, position.Longitude);
                //await map.MoveCamera(CameraUpdateFactory.NewPosition(new Position(position.Latitude, position.Longitude)));
                //await map.MoveCamera(CameraUpdateFactory.NewCameraPosition(new CameraPosition(
                //new Position(
                //    position.Latitude,
                //     position.Longitude),
                //    18d,
                //    0d,
                //    75d
                //)));
                //var previousPosition = map?.Polylines?.FirstOrDefault()?.Positions?.FirstOrDefault();
                //map.Polylines?.FirstOrDefault()?.Positions?.Remove(previousPosition.Value);
            }
            else
            {
                //END TRIP
                map.Polylines.Clear();
                map.Polylines?.FirstOrDefault()?.Positions?.Clear();
            }
        }

        async void Calculate(List<Position> list)
        {
            //searchLayout.IsVisible = false;
            
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
            //map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(polyline.Positions[0].Latitude, polyline.Positions[0].Longitude), Distance.FromMiles(0.50f)));
            await map.MoveCamera(CameraUpdateFactory.NewCameraPosition(new CameraPosition(
                new Position(
                    polyline.Positions[0].Latitude,
                    polyline.Positions[0].Longitude),
                    18d,
                    0d,
                    60d
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
            var pin1 = new Pin
            {
                Type = PinType.Place,
                Position = new Position(polyline.Positions.Last().Latitude, polyline.Positions.Last().Longitude),
                Label = "Last",
                Address = "Last",
                Tag = string.Empty,
            };
            map.Pins.Add(pin1);
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

        List<Place> placesList = new List<Place>();

        public async void OnEnterAddressTapped(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchPlacePage() { BindingContext = this.BindingContext }, false);
        }

    }
}