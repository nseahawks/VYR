using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
    
namespace VYRMobile.Models
    {
    public partial class Antena
    {
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
            }
        }
    }
}
