using System;
using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.Venues
{
    [Serializable]
    public class VenueClaimStep3Model
    {
        public VenueClaimStep3Model()
        {
            Address = new AddressEditorModel();
        }

        public VenueClaimStep3Model(Venue venue)
        {
            Address = new AddressEditorModel(venue.Address);
        }

        public AddressEditorModel Address { get; set; }

        public bool Back { get; set; }

        public void PopulateEntity(Venue venue, IAddressService addressService)
        {
            venue.Address = new Address();
            Address.PopulateEntity(venue.Address, addressService);
        }

        public VenueClaimStep3Model PopulateMetadata(IEnumerable<Country> countries)
        {
            Address.PopulateMetadata(countries);
            return this;
        }

    }
}