using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;
using Clicks.Yoga.Portal.Models.Entities.Summaries;

namespace Clicks.Yoga.Portal.Models.Search
{
    public class SearchTeacherTrainingNavigationModel
    {
        public SearchTeacherTrainingNavigationModel(SearchCriteriaModel criteria, IEnumerable<Style> styles, IEnumerable<AccreditationBody> accreditations)
        {
            Criteria = criteria;

            Styles = new List<NamedSummaryModel>();

            foreach (var style in styles)
                Styles.Add(new NamedSummaryModel(style));

            Accreditations = new List<NamedSummaryModel>();

            foreach (var accreditation in accreditations)
                Accreditations.Add(new NamedSummaryModel(accreditation));
        }

        public bool Organisations { get; set; }

        public SearchCriteriaModel Criteria { get; set; }

        public IList<NamedSummaryModel> Styles { get; set; }

        public IList<NamedSummaryModel> Accreditations { get; set; }
    }
}