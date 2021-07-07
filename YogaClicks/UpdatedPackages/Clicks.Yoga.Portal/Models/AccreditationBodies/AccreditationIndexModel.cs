using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.AccreditationBodies
{
    public class AccreditationIndexModel
    {
        public AccreditationIndexModel(IEnumerable<AccreditationBody> bodies, string country)
        {
            All = new List<AccreditationBodyModel>();
            Countries = new List<AccreditationCountryModel>();
            Country = country;

            var models = bodies.ToList();

            foreach (var body in models)
                All.Add(new AccreditationBodyModel(body));

            AccreditationBodies = All.Where(e => string.IsNullOrEmpty(country) || (e.Country != null && e.Country.Code == country)).ToList();

            var countries = models.Where(c => c.Address != null && c.Address.Country != null).Select(c => c.Address.Country).Distinct();

            foreach (var c in countries)
            {
                var temp = c;
                Countries.Add(new AccreditationCountryModel(c, models.Where(b => b.Address != null && b.Address.Country != null && b.Address.Country.Id == temp.Id).ToList()));
            }
        }

        public string Country { get; set; }

        public IList<AccreditationCountryModel> Countries { get; set; }

        public IList<AccreditationBodyModel> AccreditationBodies { get; set; }

        public IList<AccreditationBodyModel> All { get; set; }
    }
}