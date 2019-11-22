using MvvmHelpers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace VYRMobile.Models
{
    public class GooglePlace : ObservableObject
    {
        string name;
        public string Name {
            get => name;
            set => SetProperty(ref name, value);
        }
        double latitude;
        public double Latitude { 
            get => latitude;
            set => SetProperty(ref latitude, value); 
        }
        double longitude;
        public double Longitude { 
            get => longitude; 
            set => SetProperty(ref longitude, value); 
        }
        string raw;
        public string Raw {
            get => raw;
            set => SetProperty(ref raw, value);
        }
        public GooglePlace(JObject jsonObject)
        {
            Name = (string)jsonObject["result"]["name"];
            Latitude = (double)jsonObject["result"]["geometry"]["location"]["lat"];
            Longitude = (double)jsonObject["result"]["geometry"]["location"]["lng"];
            Raw = jsonObject.ToString();
        }
    }
}
