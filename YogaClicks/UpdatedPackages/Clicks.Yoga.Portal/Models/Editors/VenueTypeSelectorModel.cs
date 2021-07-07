using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Editors
{
    public class VenueTypeSelectorModel : SingleEntitySelectorModel<VenueType, VenueTypeModel>
    {
        public VenueTypeSelectorModel() {}

        public VenueTypeSelectorModel(VenueType entity) : base(entity) {}
    }
}