using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Editors
{
    public class SignUpVenueAmenityChooserModel : MultipleEntitySelectorModel<VenueAmenity, VenueAmenityModel>
    {
        public SignUpVenueAmenityChooserModel() { }

        public SignUpVenueAmenityChooserModel(IEnumerable<VenueAmenity> entities) : base(entities) { }

        public bool Residential { get; set; }

        public void PopulateCollection(ICollection<VenueAmenity> collection, IVenueService venueService, Venue venue)
        {
            PopulateCollection(collection, venueService.GetVenueAmenity);

            //foreach (var amenity in collection.ToList())
            //{
            //    if (amenity.IsResidential != venue.IsResidential) collection.Remove(amenity);
            //}
        }

        public void PopulateMetadata(IEnumerable<VenueAmenity> entities, bool residential)
        {
            Residential = residential;
            PopulateMetadata(entities);
        }
    }
}