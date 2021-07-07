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
        public SearchVenuesNavigationModel(string action, SearchCriteriaModel criteria, IEnumerable<VenueService> services, IEnumerable<VenueType> types, IEnumerable<Style> styles, IEnumerable<Condition> conditions)
        {
            Action = action;
            Criteria = criteria;

            Services = new List<VenueServiceModel>();
            Conditions = new List<NamedSummaryModel>();

            foreach (var item in services)
                Services.Add(new VenueServiceModel(item));

            foreach (var condition in conditions)
                Conditions.Add(new NamedSummaryModel(condition));

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

        public IList<NamedSummaryModel> Conditions { get; set; }
    }
}