using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;
using Clicks.Yoga.Portal.Models.Entities.Summaries;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivityDisplayModel
    {
        public ActivityDisplayModel(Activity activity)
        {
            Activity = new ActivityModel(activity);
            Teachers = new List<NamedSummaryModel>();
            Styles = new List<NamedSummaryModel>();
            Conditions = new List<NamedSummaryModel>();
            StubTeachers = new List<string>();

            foreach (var professional in activity.Teachers.Where(t => t.Teacher.Active && t.Teacher.Owner != null))
            {
                Teachers.Add(new NamedSummaryModel(professional.Teacher));
            }

            foreach (var teacher in activity.Teachers.Select(x => x.Teacher).Where(t => t.Owner == null))
            {
                StubTeachers.Add(teacher.Name);
            }

            foreach (var style in activity.Styles)
            {
                Styles.Add(new NamedSummaryModel(style));
            }

            foreach (var condition in activity.Conditions)
            {
                Conditions.Add(new NamedSummaryModel(condition));
            }
        }

        public ActivityModel Activity { get; private set; }

        public IList<NamedSummaryModel> Teachers { get; private set; }

        public List<string> StubTeachers { get; set; }

        public IList<NamedSummaryModel> Styles { get; private set; }

        public IList<NamedSummaryModel> Conditions { get; private set; }
    }
}