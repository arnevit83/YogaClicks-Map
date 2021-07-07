using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.AccreditationBodies
{
    public class AccreditationBodyDisplayModel
    {
        public AccreditationBodyDisplayModel(AccreditationBody entity, IList<AccreditationBody> all)
        {
            AccreditationBody = new AccreditationBodyModel(entity);
            Countries = new List<AccreditationCountryModel>();

            All = new List<AccreditationBodyModel>();

            foreach (var body in all)
                All.Add(new AccreditationBodyModel(body));

            
            var countries = all.Where(c => c.Address != null && c.Address.Country != null).Select(c => c.Address.Country).Distinct();

            foreach (var c in countries)
            {
                var temp = c;
                Countries.Add(new AccreditationCountryModel(c, all.Where(b => b.Address != null && b.Address.Country != null && b.Address.Country.Id == temp.Id).ToList()));
            }
        }

        public AccreditationBodyModel AccreditationBody { get; set; }

        public IList<AccreditationBodyModel> All { get; set; }

        public IList<AccreditationCountryModel> Countries { get; set; }
    }
}