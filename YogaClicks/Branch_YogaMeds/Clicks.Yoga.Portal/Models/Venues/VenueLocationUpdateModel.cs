using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Venues
{
    public class VenueLocationUpdateModel
    {
         public VenueLocationUpdateModel()
        {
            Address = new AddressEditorModel();
        }

         public VenueLocationUpdateModel(Venue venue)
        {
            VenueId = venue.Id;
            Address = new AddressEditorModel(venue.Address);
        }

        public void PopulateEntity(
            Venue entity,
             IAddressService addressService)
        {
            entity.Address = Address.PopulateEntity(entity.Address, addressService);
        }

        public int VenueId { get; set; }

        public AddressEditorModel Address { get; set; }

        public VenueLocationUpdateModel PopulateMetadata(
            Venue venue,
            IAddressService addressService)
        {
            Address.PopulateMetadata(addressService.GetCountries());
            return this;
        }
    }
}