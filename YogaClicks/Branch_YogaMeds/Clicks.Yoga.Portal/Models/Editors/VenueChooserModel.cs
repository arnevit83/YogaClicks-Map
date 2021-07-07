using System.Collections.Generic;
using System.Runtime.Serialization;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Editors
{
    [DataContract]
    public class VenueChooserModel : MultipleEntitySelectorModel<Venue, VenueModel>
    {
        public VenueChooserModel() { }

        public VenueChooserModel(ICollection<Venue> entities)
            : base(entities)
        {
            PopulateMetadata(entities);
        }

        public LocationEditorModel Location { get; private set; }

        public void PopulateCollection(ICollection<Venue> collection, IVenueService service)
        {
            PopulateCollection(collection, service.GetVenue);
        }
    }
}