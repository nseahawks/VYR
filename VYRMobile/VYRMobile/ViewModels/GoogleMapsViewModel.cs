﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
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

namespace VYRMobile.ViewModels
{
    public class GoogleMapsViewModel : BaseViewModel
    {
        public Command CalculateRouteCommand { get; set; }
        public Command UpdatePositionCommand { get; set; }
        public Command LoadRouteCommand { get; set; }
        public Command StopRouteCommand { get; set; }
        public Command ActualLocationCommand { get; set; }

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
            
        public string _originLatitud { get; set; }
        public string _originLongitud { get; set; }
        string _destinationLatitud;
        string _destinationLongitud;

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
        bool _isPickupFocused = true;

        string _pickupText;
        public string PickupText
        {
            get
            {
                return _pickupText;
            }
            set
            {
                _pickupText = value;
                if (!string.IsNullOrEmpty(_pickupText))
                {
                    _isPickupFocused = true;
                    GetPlacesCommand.Execute(_pickupText);
                }
            }
        }

        string _originText;
        public string OriginText
        {
            get
            {
                return _originText;
            }
            set
            {
                _originText = value;
                if (!string.IsNullOrEmpty(_originText))
                {
                    _isPickupFocused = true;
                    GetPlacesCommand.Execute(_originText);
                }
            }
        }

        public Command GetLocationNameCommand { get; set; }
        public bool IsRouteNotRunning
        {
            get
            {
                return !HasRouteRunning;
            }
        }

        public GoogleMapsViewModel()
        {
            //CalculateRouteCommand = new Command(async () => await Calculate());
            LoadRouteCommand = new Command(async () => await LoadRoute());
            StopRouteCommand = new Command(StopRoute);
            GetPlacesCommand = new Command<string>(async (param) => await GetPlacesByName(param));
            GetPlaceDetailCommand = new Command<GooglePlaceAutoCompletePrediction>(async (param) => await GetPlacesDetail(param));
            GetLocationNameCommand = new Command<Position>(async (param) => await GetLocationName(param));
        }




        public async Task LoadRoute()
        {
            var positionIndex = 1;
            ActualLocationCommand.Execute(null);
            var googleDirection = await googleMapsApi.GetDirections(
               _originLatitud, _originLongitud,
                "18.461294", "-69.948531"
                );
            if (googleDirection.Routes != null && googleDirection.Routes.Count > 0)
            {
                var positions = (Enumerable.ToList(PolylineHelper.Decode(googleDirection.Routes.First().OverviewPolyline.Points)));
                CalculateRouteCommand.Execute(positions);

                HasRouteRunning = true;

                //Location tracking simulation
                Device.StartTimer(TimeSpan.FromSeconds(1), () =>
                {
                    if (positions.Count > positionIndex && HasRouteRunning)
                    {
                        UpdatePositionCommand.Execute(positions[positionIndex]);
                        //positionIndex++;
                        return true;
                    }
                    else
                    {
                            App.Current.MainPage.DisplayAlert(":)", "Has llegado a tu destino.", "Ok");
                        //if (positions.Count <= positionIndex && !HasRouteRunning)
                        //{
                        //}
                        //else
                        //{
                        //    App.Current.MainPage.DisplayAlert(":(", "Tu ruta se ha cancelado, presion 'Start Route' para inicar una nueva ruta.", "Ok");
                        //}
                           
                        return false;
                    }
                });
            }
            else
            {
                await App.Current.MainPage.DisplayAlert(":)", "No route found", "Ok");
            }

        }
        public void StopRoute()
        {
            HasRouteRunning = false;
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

        public async Task GetPlacesDetail(GooglePlaceAutoCompletePrediction placeA)
        {
            var place = await googleMapsApi.GetPlaceDetails(placeA.PlaceId);
            if (place != null)
            {
                if (_isPickupFocused)
                {
                    PickupText = place.Name;
                    _originLatitud = $"{place.Latitude}";
                    _originLongitud = $"{place.Longitude}";
                    _isPickupFocused = false;
                    FocusOriginCommand.Execute(null);
                }
                else
                {
                    _destinationLatitud = $"{place.Latitude}";
                    _destinationLongitud = $"{place.Longitude}";

                    RecentPlaces.Add(placeA);

                    if (_originLatitud == _destinationLatitud && _originLongitud == _destinationLongitud)
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "Origin route should be different than destination route", "Ok");
                    }
                    else
                    {
                        LoadRouteCommand.Execute(null);
                        await App.Current.MainPage.Navigation.PopAsync(false);
                        CleanFields();
                    }

                }
            }
        }

        void CleanFields()
        {
            PickupText = OriginText = string.Empty;
            ShowRecentPlaces = true;
            PlaceSelected = null;
        }


        //Get place 
        public async Task GetLocationName(Position position)
        {
            try
            {
                var placemarks = await Geocoding.GetPlacemarksAsync(position.Latitude, position.Longitude);
                var placemark = placemarks?.FirstOrDefault();
                if (placemark != null)
                {
                    PickupText = placemark.FeatureName;
                }
                else
                {
                    PickupText = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        //public event PropertyChangedEventHandler PropertyChanged;

    }
}
