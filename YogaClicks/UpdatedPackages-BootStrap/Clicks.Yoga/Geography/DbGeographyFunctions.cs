using System;
using System.Data.Spatial;

namespace Clicks.Yoga.Geography
{
    public static class DbGeographyFunctions
    {
        public static DbGeography PointToGeography(GeographicPoint point)
        {
            if (point == null) return null;
            return DbGeography.PointFromText(string.Format("POINT({0} {1})", point.Longitude, point.Latitude), GeographicPoint.CoordinateSystem);
        }

        public static GeographicPoint GeographyToPoint(DbGeography geography)
        {
            if (geography == null) return null;
            if (!geography.Latitude.HasValue || !geography.Longitude.HasValue) throw new ArgumentException("Invalid geography.");
            return new GeographicPoint(geography.Latitude.Value, geography.Longitude.Value);
        }
    }
}