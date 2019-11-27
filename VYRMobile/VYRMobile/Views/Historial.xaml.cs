using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VYRMobile.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace VYRMobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Historial : ContentPage
    {
        
        public Historial()
        {
            InitializeComponent();
            BindingContext = new CallViewModel();
            BindingContext = new CronoViewModel();
            btnStart.Clicked += BtnStart_Clicked;
            btnStop.Clicked += BtnStop_Clicked;
           
            /*Menu.ItemTapped += async (sender, e) =>
            {
                var evnt = (SelectedItemChangedEventArgs)e;
                Notifier.Text = (string)evnt.SelectedItem;
                await Task.Delay(2000);
                Notifier.Text = "";

            };*/
        }

        private void BtnStop_Clicked(object sender, EventArgs e)
        {
            btnStop.IsVisible = false;
            btnStart.IsVisible = true;
        }

        private void BtnStart_Clicked(object sender, EventArgs e)
        {
            btnStart.IsVisible = false;
            btnStop.IsVisible = true;
        }

        /*async void OnButtonClicked(object sender, EventArgs e)
            {
                Geocoder geoCoder = new Geocoder();
                try
                {
                    var locator = CrossGeolocator.Current;
                    locator.DesiredAccuracy = 50;
                    var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(2));
                    double? latitude = Convert.ToDouble(position.Latitude);
                    double? longitude = Convert.ToDouble(position.Longitude);
                    if (latitude != null && longitude != null)
                    {
                        var revposition = new Position(latitude.Value, longitude.Value);
                        var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(revposition);
                        foreach (var address in possibleAddresses)
                            addressLabel.Text += address + "\n";
                    }
                    else addressLabel.Text += "error";
                }
                catch (Exception ex)
                {
                    await DisplayAlert("Notification", "Unable to get GPS Location " + ex, "Ok");
                }
            }*/
    }
}