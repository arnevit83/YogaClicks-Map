using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Venues
{
    public class VenueAmenitiesUpdateModel
    {
          public VenueAmenitiesUpdateModel()
        {
            Amenities = new VenueAmenityChooserModel();
        }

          public VenueAmenitiesUpdateModel(Venue venue)
        {
            VenueId = venue.Id;
            Amenities = new VenueAmenityChooserModel(venue.Amenities);
        }

        public void PopulateEntity(
            Venue entity,
            IVenueService venueService)
        {
            Amenities.PopulateCollection(entity.Amenities, venueService, entity);
        }

        public int VenueId { get; set; }

        public VenueAmenityChooserModel Amenities { get; private set; }

        public VenueAmenitiesUpdateModel PopulateMetadata(
            Venue venue,
            IVenueService venueService)
        {
            Amenities.PopulateMetadata(venueService.GetVenueAmenities(), venue.IsResidential);
            return this;
        }
    }
}