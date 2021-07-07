using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.AccreditationBodies
{
    public class AccreditationCountryModel
    {
        public AccreditationCountryModel(Country country, IList<AccreditationBodyModel> bodies)
        {
            Country = new CountryModel(country);

            AccreditationBodies = bodies;
        }

        public AccreditationCountryModel(Country country, IList<AccreditationBody> bodies)
        {
            Country = new CountryModel(country);

            AccreditationBodies = new List<AccreditationBodyModel>();

            foreach (var body in bodies)
                AccreditationBodies.Add(new AccreditationBodyModel(body));
        }

        public CountryModel Country { get; set; }

        public IList<AccreditationBodyModel> AccreditationBodies { get; set; }
    }
}