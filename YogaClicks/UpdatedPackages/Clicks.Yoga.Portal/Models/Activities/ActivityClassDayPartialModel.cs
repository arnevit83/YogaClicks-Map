using System;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivityClassDayPartialModel
    {
        public ActivityClassDayPartialModel(DateTime date, ActivitySearchCriteriaModel criteria)
        {
            Date = date;
            Criteria = criteria;
        }

        public DateTime Date { get; private set; }

        public ActivitySearchCriteriaModel Criteria { get; private set; }
    }
}