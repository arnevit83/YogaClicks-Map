using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Geography;

namespace Clicks.Yoga.Portal.Models.Editors
{
    public class YogaMapAddressEditorModel
    {
        public YogaMapAddressEditorModel()
        {
            Country = new CountrySelectorModel();
        }

        public YogaMapAddressEditorModel(Address address) : this()
        {
            if (address == null) return;

            Line1 = address.Line1;
            Line2 = address.Line2;
            City = address.City;
            Area = address.Area;
            Country = new CountrySelectorModel(address.Country);
            Postcode = address.Postcode;

            if (address.Location != null)
            {
                Latitude =  address.Location.Latitude;
                Longitude = address.Location.Longitude;
            }
        }

        public YogaMapAddressEditorModel(Address address, IEnumerable<Country> countries)
            : this(address)
        {
            PopulateMetadata(countries);
        }

        public Address PopulateEntity(Address entity, IAddressService addressService)
        {
            if (entity == null) entity = new Address();

            entity.Line1 = Line1;
            entity.Line2 = Line2;
            entity.City = City;
            entity.Area = Area;
            entity.Country = Country.Selected ? addressService.GetCountry(Country.Id) : null;
            entity.Postcode = Postcode;
            entity.Location = new GeographicPoint(Latitude, Longitude);

            return entity;
        }

        public Address PopulateEntity(IAddressService addressService)
        {
            return PopulateEntity(null, addressService);
        }

        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string City { get; set; }

        public string Area { get; set; }

        public CountrySelectorModel Country { get; set; }

        public string Postcode { get; set; }

       [Required(ErrorMessageResourceType = typeof(Resources.Validation.YogaMap), ErrorMessageResourceName = "This_is_required_address")]
        [RegularExpression(@"-?\d+\.\d+", ErrorMessageResourceType = typeof(Resources.Validation.YogaMap), ErrorMessageResourceName = "This_is_required_address")] 
    
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public void PopulateMetadata(IEnumerable<Country> countries)
        {
            Country.PopulateMetadata(countries);
        }
    }
}