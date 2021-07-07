using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class VenueTypeModel : EntityModel<VenueType>
    {
        public VenueTypeModel() {}

        public VenueTypeModel(VenueType entity) : base(entity) {}

        public string Name { get; set; }

        public bool Residential { get; set; }

        public override void Populate(VenueType entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Residential = entity.IsResidential;
        }
    }
}