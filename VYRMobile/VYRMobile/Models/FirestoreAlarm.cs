using Plugin.CloudFirestore;
using Plugin.CloudFirestore.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace VYRMobile.Models
{
    public class FirestoreAlarm
    {
        [MapTo("LocationName")]
        public string LocationName { get; set; }

        [MapTo("Type")]
        public string Type { get; set; }

        [MapTo("Location")]
        public GeoPoint Location { get; set; }
    }
}
