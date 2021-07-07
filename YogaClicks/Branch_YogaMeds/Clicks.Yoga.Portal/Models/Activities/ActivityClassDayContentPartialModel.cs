using System;
using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivityClassDayContentPartialModel
    {
        public ActivityClassDayContentPartialModel(DateTime date, ActivitySearchCriteriaModel criteria, IList<ActivityRepeatInstance> repeats)
        {
            Date = date;
            Criteria = criteria;
            Repeats = new ActivityRepeatInstanceListModel(repeats);
        }

        public DateTime Date { get; private set; }

        public ActivitySearchCriteriaModel Criteria { get; private set; }

        public ActivityRepeatInstanceListModel Repeats { get; private set; }

        public string DayName
        {
            get
            {
                var nextWeek = DateTime.Now.Date.AddDays(1);

                while (nextWeek.DayOfWeek != DayOfWeek.Monday)
                {
                    nextWeek = nextWeek.AddDays(1);
                }

                if (Date == DateTime.Now.Date)
                {
                    return "Today";
                }
                if (Date == DateTime.Now.Date.AddDays(1))
                {
                    return "Tomorrow";
                }
                else if (Date < nextWeek)
                {
                    return string.Format("This {0}", Date.DayOfWeek);
                }
                else if (Date < nextWeek.AddDays(7))
                {
                    return string.Format("Next {0}", Date.DayOfWeek);
                }
                else
                {
                    return Date.ToString("dddd dd MMMM");
                }
            }
        }
    }
}