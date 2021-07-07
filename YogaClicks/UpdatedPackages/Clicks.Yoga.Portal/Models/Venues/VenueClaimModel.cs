using System;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;

namespace Clicks.Yoga.Portal.Models.Venues
{
    [Serializable]
    public class VenueClaimModel
    {
        public VenueClaimModel(int venueId)
        {
            VenueId = venueId;
        }

        public int VenueId { get; private set; }

        public VenueClaimStep1Model Step1Model { get; set; }

        public VenueClaimStep2Model Step2Model { get; set; }

        public VenueClaimStep3Model Step3Model { get; set; }

        public void PopulateEntity(
            Venue entity,
            IWebsiteService websiteService,
            IAddressService addressService)
        {
            Step2Model.PopulateEntity(entity, websiteService);
            Step3Model.PopulateEntity(entity, addressService);
        }
    }
}