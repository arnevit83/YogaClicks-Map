using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class LocationModel : EntityModel<Location>
    {
        public LocationModel(Location entity) : base(entity) { }

        public string Name { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public override void Populate(Location entity)
        {
            Name = entity.Name;
            Latitude = entity.Coordinates.Latitude;
            Longitude = entity.Coordinates.Longitude;
        }
    }
}