using System;
using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;
using Common;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivityClassWeekContentPartialModel
    {
        public ActivityClassWeekContentPartialModel(DateTime date, ActivitySearchCriteriaModel criteria, IList<ActivityRepeatInstance> repeats, bool editable)
        {
            Date = date.Date;
            Criteria = criteria;
            Editable = editable;
            Days = new List<ActivityClassDayModel>();

            var list = new ActivityRepeatInstanceListModel(repeats);
            var day = Date;

            while (day == Date || day.DayOfWeek != DayOfWeek.Monday)
            {
                Days.Add(new ActivityClassDayModel(day, list.Where(r => r.StartTime.Date == day).ToList()));
                day = day.AddDays(1);
            }
        }

        public DateTime Date { get; private set; }

        public ActivitySearchCriteriaModel Criteria { get; private set; }

        public IList<ActivityClassDayModel> Days { get; private set; }

        public bool Editable { get; private set; }

        public string WeekName
        {
            get
            {
                var difference = (Date - DateTime.Now.WeekBegin(DayOfWeek.Monday)).TotalDays;

                if (difference < -7)
                {
                    return string.Format("{0} weeks ago", Convert.ToInt32(Math.Abs(difference / 7)));
                }
                else if (difference < 0)
                {
                    return "Last week";
                }
                else if (difference < 7)
                {
                    return "This week";
                }
                else if (difference < 14)
                {
                    return "Next week";
                }
                else
                {
                    return string.Format("{0} weeks ahead", Convert.ToInt32(difference / 7));
                }
            }
        }
    }
}