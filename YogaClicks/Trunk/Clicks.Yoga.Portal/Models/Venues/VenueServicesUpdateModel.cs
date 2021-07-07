using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Venues
{
    public class VenueServicesUpdateModel
    {
        public VenueServicesUpdateModel()
        {
            Services = new VenueServiceChooserModel();
        }

        public VenueServicesUpdateModel(Venue venue)
        {
            VenueId = venue.Id;
            Services = new VenueServiceChooserModel(venue.Services);
        }

        public void PopulateEntity(
            Venue entity,
             IVenueService venueService)
        {
            Services.PopulateCollection(entity.Services, venueService);
        }

        public int VenueId { get; set; }

        public VenueServiceChooserModel Services { get; private set; }

        public VenueServicesUpdateModel PopulateMetadata(
            Venue venue,
            IVenueService venueService)
        {
            Services.PopulateMetadata(venueService.GetVenueServices());
            return this;
        }
    }
}