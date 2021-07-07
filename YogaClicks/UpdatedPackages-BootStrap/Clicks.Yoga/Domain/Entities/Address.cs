using System;
using System.Collections.Generic;
using System.Data.Spatial;
using Clicks.Yoga.Geography;

namespace Clicks.Yoga.Domain.Entities
{
    public class Address : Entity
    {
        public Address() {}

        public Address(Country country)
        {
            Country = country;
        }

        public string Line1 { get; set; }

        public string Line2 { get; set; }

        public string Line3 { get; set; }

        public string City { get; set; }

        public string Area { get; set; }

        public string Postcode { get; set; }

        public virtual Country Country { get; set; }

        public GeographicPoint Location { get; set; }

        public DbGeography LocationGeography
        {
            get { return DbGeographyFunctions.PointToGeography(Location); }
            set { Location = DbGeographyFunctions.GeographyToPoint(value); }
        }

        public string LocationName
        {
            get
            {
                var parts = new List<string>();

                if (!string.IsNullOrWhiteSpace(City)) parts.Add(City);
                if (!string.IsNullOrWhiteSpace(Area)) parts.Add(Area);

                return string.Join(", ", parts);
            }
        }

        public override string ToString()
        {
            return ToSingleLineString();
        }

        public string ToSingleLineString()
        {
            return ToString(", ");
        }

        public string ToMultiLineString()
        {
            return ToString(Environment.NewLine);
        }

        public string ToString(string separator)
        {
            var parts = new List<string>();

            if (Line1 != null) parts.Add(Line1);
            if (Line2 != null) parts.Add(Line2);
            if (Line3 != null) parts.Add(Line3);
            if (City != null) parts.Add(City);
            if (Area != null) parts.Add(Area);
            if (Country != null) parts.Add(Country.EnglishName);
            if (Postcode != null) parts.Add(Postcode);

            return string.Join(separator, parts);
        }
    }
}