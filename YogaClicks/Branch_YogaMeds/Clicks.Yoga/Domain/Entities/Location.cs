using System.Data.Spatial;
using Clicks.Yoga.Geography;

namespace Clicks.Yoga.Domain.Entities
{
    public class Location : Entity
    {
        public string Name { get; set; }

        public GeographicPoint Coordinates { get; set; }

        public DbGeography Geography
        {
            get { return DbGeographyFunctions.PointToGeography(Coordinates); }
            set { Coordinates = DbGeographyFunctions.GeographyToPoint(value); }
        }
    }
}