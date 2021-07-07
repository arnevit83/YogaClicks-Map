namespace Clicks.Yoga.Geography
{
    public class GeographicBounds
    {
        public GeographicBounds(double north, double south, double west, double east)
        {
            NorthEast = new GeographicPoint(north, east);
            SouthWest = new GeographicPoint(south, west);
        }

        public GeographicPoint NorthEast { get; private set; }

        public GeographicPoint SouthWest { get; private set; }

        public string ToWkt()
        {
            return string.Format(
                "POLYGON(({0} {1}, {2} {1}, {2} {3}, {0} {3}, {0} {1}))",
                NorthEast.Longitude, NorthEast.Latitude,
                SouthWest.Longitude, SouthWest.Latitude
            );
        }
    }
}