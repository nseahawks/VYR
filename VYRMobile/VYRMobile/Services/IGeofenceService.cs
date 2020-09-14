using System.Collections.Generic;
using Xamarin.Forms.GoogleMaps;

namespace VYRMobile.Services
{
    public interface IGeofenceService
    {
        void SetGeofences(List<Position> targets, Position currentPosition);
    }
}
