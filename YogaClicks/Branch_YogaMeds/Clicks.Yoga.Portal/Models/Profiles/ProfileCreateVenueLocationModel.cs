using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.Profiles
{
    public class ProfileCreateVenueLocationModel
    {        
        public ProfileCreateVenueLocationModel()
        {
            Address = new AddressEditorModel();
        }

        public AddressEditorModel Address { get; set; }

        public void PopulateEntity(Venue venue, IAddressService addressService)
        {
            venue.Address = new Address();
            Address.PopulateEntity(venue.Address, addressService);
        }

        public ProfileCreateVenueLocationModel PopulateMetadata(IEnumerable<Country> countries)
        {
            Address.PopulateMetadata(countries);
            return this;
        }
    }
}