using System;
using System.ComponentModel;
using Newtonsoft.Json;

namespace VYRMobile.Models
{
    public partial class CompanyLocation : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            {
                if (changed != null)
                    PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("locationName")]
        public string LocationName { get; set; }

        [JsonProperty("cellId")]
        public string CellId { get; set; }

        [JsonProperty("address")]
        public string Address { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("userId")]
        public object UserId { get; set; }

        [JsonProperty("locationType")]
        public object LocationType { get; set; }

        [JsonProperty("getRoutes")]
        public object[] GetRoutes { get; set; }

        [JsonProperty("getAlarm")]
        public object GetAlarm { get; set; }

        private bool pointChecked;
        public bool PointChecked
        {
            get => pointChecked;
            set
            {
                pointChecked = value;
                OnPropertyChanged("PointChecked");
            }
        }
    }
}