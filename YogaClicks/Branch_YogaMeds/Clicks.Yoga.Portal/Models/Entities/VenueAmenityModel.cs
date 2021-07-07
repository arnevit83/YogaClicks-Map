using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class VenueAmenityModel : EntityModel<VenueAmenity>
    {
        public VenueAmenityModel() { }

        public VenueAmenityModel(VenueAmenity entity) : base(entity) { }

        public string Name { get; set; }

        public bool IsResidential { get; set; }

        public override void Populate(VenueAmenity entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            IsResidential = entity.IsResidential;
        }
    }
}