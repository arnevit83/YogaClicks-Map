using System.Collections.Generic;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Entities;
using VenueService = Clicks.Yoga.Domain.Entities.VenueService;

namespace Clicks.Yoga.Portal.Models.Editors
{
    public class SignUpVenueServiceChooserModel : MultipleEntitySelectorModel<VenueService, VenueServiceModel>
    {
        public SignUpVenueServiceChooserModel() {}

        public SignUpVenueServiceChooserModel(IEnumerable<VenueService> entities) : base(entities) { }

        public void PopulateCollection(ICollection<VenueService> collection, IVenueService venueService)
        {
            PopulateCollection(collection, venueService.GetVenueService);
        }
    }
}