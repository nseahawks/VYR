using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.GoogleMaps;

namespace VYRMobile.Controls
{
    public class CustomMap : Map
    {
        public new List<CustomCircle> Circles { get; set; }
    }
}
