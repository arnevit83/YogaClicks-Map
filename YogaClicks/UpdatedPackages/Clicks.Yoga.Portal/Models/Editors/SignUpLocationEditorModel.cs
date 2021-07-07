using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Geography;

namespace Clicks.Yoga.Portal.Models.Editors
{
    public class SignUpLocationEditorModel
    {
        public SignUpLocationEditorModel() {}

        public SignUpLocationEditorModel(Location entity)
        {
            if (entity == null) return;

            Name = entity.Name;
            Latitude = entity.Coordinates.Latitude;
            Longitude = entity.Coordinates.Longitude;
        }

        public string Name { get; set; }

        public bool ShowCityOnly { get; set; }

        public double? Latitude { get; set; }

        public double? Longitude { get; set; }

        public bool HasValue
        {
            get { return Latitude.HasValue && Longitude.HasValue; }
        }

        public Location PopulateEntity(Location entity)
        {
            if (!HasValue) return null;
            if (entity == null) entity = new Location();
            entity.Name = Name;
            entity.Coordinates = new GeographicPoint(Latitude.Value, Longitude.Value);
            return entity;
        }
    }
}