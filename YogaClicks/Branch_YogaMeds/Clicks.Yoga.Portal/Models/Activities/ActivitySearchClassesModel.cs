using System;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivitySearchClassesModel
    {
        public ActivitySearchClassesModel(ActivitySearchCriteriaModel criteria)
        {
            Date = criteria.StartDate.HasValue ? criteria.StartDate.Value.Date : DateTime.Now.Date;
            Criteria = criteria;
        }

        public DateTime Date { get; private set; }

        public ActivitySearchCriteriaModel Criteria { get; private set; }
    }
}