using Xamarin.Essentials;

namespace VYRMobile.Helper
{
    public class LineHelper
    {
        public double M(double initLat, double initLong, double finalLat, double finalLong)
        {
            var m = (finalLat - initLat) / (finalLong - initLong);
            return m;
        }

        public double Distance(Location init, Location final)
        {
            double kilometers = Location.CalculateDistance(init, final, DistanceUnits.Kilometers);
            return kilometers*1000;
        }

        public Location Intersection(double yP, double xP, double initLat, double initLong, double finalLat, double finalLong)
        {
            var m = M(initLat, initLong, finalLat, finalLong);
            var newX = ((m * initLong) - initLat + yP + ((1 / m) * xP)) / (m + (1 / m));
            var newY = ((-1 / m) * newX) + yP + ((1 / m) * xP);
            return new Location(newY, newX);
        }

        public bool OutOfBound(Location intersection,double initLat, double initLong, double finalLat, double finalLong)
        {
            var minLat = min_val(initLat,finalLat);
            var maxLat = max_val(initLat, finalLat);
            var minLng = min_val(initLong, finalLong);
            var maxLng = max_val(initLong, finalLong);
            if(intersection.Latitude >= minLat && intersection.Latitude <= maxLat 
                && intersection.Longitude >= minLng && intersection.Longitude <= maxLng)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public double min_val(double x, double y)
        {
            if(x > y)
            {
                return y;
            }
            else
            {
                return x;
            }
        }
        public double max_val(double x, double y)
        {
            if(x < y)
            {
                return y;
            }
            else
            {
                return x;
            }
        }
    }
}
