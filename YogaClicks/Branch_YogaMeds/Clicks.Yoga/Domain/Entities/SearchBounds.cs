namespace Clicks.Yoga.Domain.Entities
{
    public class SearchBounds
    {
        public SearchBounds(decimal north, decimal south, decimal west, decimal east)
        {
            NorthEast = new SearchCoordinates(north, east);
            SouthWest = new SearchCoordinates(south, west);
        }

        public SearchCoordinates NorthEast { get; private set; }

        public SearchCoordinates SouthWest { get; private set; }

        public string ToWkt()
        {
            var east = NorthEast.Longitude;
            var west = SouthWest.Longitude;

            if (east == -west) east = east + 0.001m;

            return string.Format(
                "POLYGON(({0} {1}, {2} {1}, {2} {3}, {0} {3}, {0} {1}))",
                east, NorthEast.Latitude,
                west, SouthWest.Latitude
            );
        }
    }
}