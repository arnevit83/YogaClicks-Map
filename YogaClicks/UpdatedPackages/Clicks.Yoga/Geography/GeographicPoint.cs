using Newtonsoft.Json;
using System;

namespace Clicks.Yoga.Geography
{
    public class GeographicPoint
    {
        public const int CoordinateSystem = 4326;

        private readonly double _latitude;
        private readonly double _longitude;

        public GeographicPoint(double latitude, double longitude)
        {
            _latitude = latitude;
            _longitude = longitude;
        }

        [JsonProperty(PropertyName="Lat")]
        public double Latitude
        {
            get { return _latitude; }
        }

        [JsonProperty(PropertyName = "Lon")]
        public double Longitude
        {
            get { return _longitude; }
        }

        public string ToWkt()
        {
            return string.Format("POINT({0} {1})", Longitude, Latitude);
        }
    }
}