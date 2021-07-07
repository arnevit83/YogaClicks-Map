using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Editors;
using Clicks.Yoga.Portal.Models.Entities;
using Clicks.Yoga.Portal.Models.Entities.Summaries;

namespace Clicks.Yoga.Portal.Models.Search
{
    public class SearchVenuesNavigationModel
    {
        public SearchVenuesNavigationModel(string action, SearchCriteriaModel criteria, IEnumerable<VenueService> services, IEnumerable<VenueType> types, IEnumerable<Style> styles)
        {
            Action = action;
            Criteria = criteria;

            Services = new List<VenueServiceModel>();

            foreach (var item in services)
                Services.Add(new VenueServiceModel(item));

            var venueTypes = types as IList<VenueType> ?? types.ToList();

            VenueTypes = new VenueTypeSelectorModel(venueTypes.FirstOrDefault(t => t.Id == criteria.VenueType));
            VenueTypes.PopulateMetadata(venueTypes);

            Styles = new List<NamedSummaryModel>();

            foreach (var style in styles)
                Styles.Add(new NamedSummaryModel(style));
        }

        public string Action { get; set; }

        public SearchCriteriaModel Criteria { get; set; }

        public IList<VenueServiceModel> Services { get; set; }

        public VenueTypeSelectorModel VenueTypes { get; set; }

        public IList<NamedSummaryModel> Styles { get; set; }
    }
}