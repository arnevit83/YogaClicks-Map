namespace Clicks.Yoga.Domain.Entities
{
    public class SearchCoordinates
    {
        public SearchCoordinates(decimal latitude, decimal longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public decimal Latitude { get; private set; }

        public decimal Longitude { get; private set; }

        public string ToWkt()
        {
            return string.Format("POINT({0} {1})", Longitude, Latitude);
        }
    }
}