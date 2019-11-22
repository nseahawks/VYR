using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VYRMobile.Models;

namespace VYRMobile.Services
{
    public interface IGoogleMapsApiService
    {
        Task<GoogleDirection> GetDirections(string originLatitude, string originLongitude, string destinationLatitude, string destinationLongitude);
        Task<GooglePlaceAutoCompleteResult> GetPlaces(string text);
        Task<GooglePlace> GetPlaceDetails(string placeId);
    }
}
