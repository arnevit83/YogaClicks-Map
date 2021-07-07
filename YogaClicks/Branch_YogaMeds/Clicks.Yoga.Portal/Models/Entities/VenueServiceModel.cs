using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class VenueServiceModel : EntityModel<VenueService>
    {
        public VenueServiceModel() {}

        public VenueServiceModel(VenueService entity) : base(entity) {}

        public string Name { get; set; }

        public int DisplayOrder { get; set; }

        public override void Populate(VenueService entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            DisplayOrder = entity.DisplayOrder;
        }
    }
}