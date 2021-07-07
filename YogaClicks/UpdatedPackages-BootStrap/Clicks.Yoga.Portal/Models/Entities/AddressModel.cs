using System.Text;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class AddressModel : EntityModel<Address>
    {
        public AddressModel(Address entity) : base(entity) {}

        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string City { get; set; }

        public string Area { get; set; }

        public string Postcode { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public CountryModel Country { get; set; }

        public string CompleteString { get; set; }

        public override void Populate(Address entity)
        {
            Id = entity.Id;
            Line1 = entity.Line1;
            Line2 = entity.Line2;
            City = entity.City;
            Area = entity.Area;
            Postcode = entity.Postcode;

            if (entity.Location != null)
            {
                Latitude = entity.Location.Latitude;
                Longitude = entity.Location.Longitude;
            }

            if (entity.Country != null) Country = new CountryModel(entity.Country);

            CompleteString = entity.ToMultiLineString();
        }
    }
}