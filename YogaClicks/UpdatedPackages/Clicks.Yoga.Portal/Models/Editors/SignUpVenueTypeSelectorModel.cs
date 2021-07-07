using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Editors
{
    public class SignUpVenueTypeSelectorModel : SingleEntitySelectorModel<VenueType, VenueTypeModel>
    {
        public SignUpVenueTypeSelectorModel() {}

        public SignUpVenueTypeSelectorModel(VenueType entity) : base(entity) { }
    }
}