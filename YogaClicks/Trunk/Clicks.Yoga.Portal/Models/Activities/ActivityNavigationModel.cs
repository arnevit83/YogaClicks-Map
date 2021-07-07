using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;
using Clicks.Yoga.Portal.Models.Entities.Summaries;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivityNavigationModel
    {
        public ActivityNavigationModel(
            ActivitySearchCriteriaModel criteria,
            IEnumerable<ActivityType> types,
            IEnumerable<Style> styles,
            IEnumerable<Condition> conditions,
            IEnumerable<AbilityLevel> levels)
        {
            Criteria = criteria;
            Types = new List<ActivityTypeModel>();
            Styles = new List<NamedSummaryModel>();
            Conditions = new List<NamedSummaryModel>();
            AbilityLevels = new List<NamedSummaryModel>();

            foreach (var type in types)
            {
                Types.Add(new ActivityTypeModel(type));
            }

            foreach (var style in styles)
            {
                Styles.Add(new NamedSummaryModel(style));
            }

            foreach (var condition in conditions)
                Conditions.Add(new NamedSummaryModel(condition));

            foreach (var level in levels)
            {
                AbilityLevels.Add(new NamedSummaryModel(level));
            }
        }

        public ActivitySearchCriteriaModel Criteria { get; private set; }

        public IList<ActivityTypeModel> Types { get; private set; }

        public IList<NamedSummaryModel> Styles { get; private set; }

        public IList<NamedSummaryModel> Conditions { get; private set; }

        public IList<NamedSummaryModel> AbilityLevels { get; private set; }

        public SelectList DayOfWeekOptions
        {
            get
            {
                var items = Enum.GetNames(typeof(DayOfWeek)).Select(v => new SelectListItem { Value = v, Text = v });
                return new SelectList(items, "Value", "Text", Criteria.DayOfWeek);
            }
        }

        public SelectList TimeOfDayOptions
        {
            get
            {
                var items = Enum.GetNames(typeof(ActivityTimeOfDay)).Select(v => new SelectListItem { Value = v, Text = v });
                return new SelectList(items, "Value", "Text", Criteria.TimeOfDay);
            }
        }

        public SelectList DurationOptions
        {
            get
            {
                var items = new SelectListItem[]
                {
                    new SelectListItem { Value = "00:45", Text = "45 minutes" },
                    new SelectListItem { Value = "01:00", Text = "60 minutes" },
                    new SelectListItem { Value = "01:30", Text = "90 minutes" },
                    new SelectListItem { Value = "02:00", Text = "120 minutes" },
                    new SelectListItem { Value = "02:30", Text = "180 minutes" }
                };

                return new SelectList(items, "Value", "Text", Criteria.Duration);
            }
        }
    }
}