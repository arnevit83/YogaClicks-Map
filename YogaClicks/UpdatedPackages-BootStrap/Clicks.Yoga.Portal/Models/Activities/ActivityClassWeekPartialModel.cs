using System;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivityClassWeekPartialModel
    {
        public ActivityClassWeekPartialModel(DateTime date, ActivitySearchCriteriaModel criteria, bool editable)
        {
            Date = date;
            Criteria = criteria;
            Editable = editable;
        }

        public ActivitySearchCriteriaModel Criteria { get; private set; }

        public DateTime Date { get; private set; }

        public bool Editable { get; private set; }
    }
}