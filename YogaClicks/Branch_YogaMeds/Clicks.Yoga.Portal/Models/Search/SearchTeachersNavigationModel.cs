using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;
using Clicks.Yoga.Portal.Models.Entities.Summaries;

namespace Clicks.Yoga.Portal.Models.Search
{
    public class SearchTeachersNavigationModel
    {
        public SearchTeachersNavigationModel(string action, SearchCriteriaModel criteria, IEnumerable<TeacherService> services, IEnumerable<Style> styles, IEnumerable<Condition> conditions, IEnumerable<AccreditationBody> accreditations)
        {
            Action = action;
            Criteria = criteria;

            Services = new List<TeacherServiceModel>();

            foreach (var item in services)
                Services.Add(new TeacherServiceModel(item));

            Styles = new List<NamedSummaryModel>();
            Conditions = new List<NamedSummaryModel>();

            foreach (var style in styles)
                Styles.Add(new NamedSummaryModel(style));

            foreach (var condition in conditions)
                Conditions.Add(new NamedSummaryModel(condition));

            Accreditations = new List<NamedSummaryModel>();

            foreach (var accreditation in accreditations)
                Accreditations.Add(new NamedSummaryModel(accreditation));
        }

        public string Action { get; set; }

        public SearchCriteriaModel Criteria { get; set; }

        public IList<TeacherServiceModel> Services { get; set; }

        public IList<NamedSummaryModel> Styles { get; set; }

        public IList<NamedSummaryModel> Conditions { get; set; }

        public IList<NamedSummaryModel> Accreditations { get; set; }
    }
}