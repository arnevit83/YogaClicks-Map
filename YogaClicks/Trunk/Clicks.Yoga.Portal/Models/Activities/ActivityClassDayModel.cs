using System;
using System.Collections.Generic;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivityClassDayModel
    {
        public ActivityClassDayModel(DateTime date, IEnumerable<ActivityRepeatInstanceModel> repeats)
        {
            Date = date;
            Repeats = new List<ActivityRepeatInstanceModel>();

            foreach (var repeat in repeats)
            {
                Repeats.Add(repeat);
            }
        }

        public DateTime Date { get; private set; }

        public IList<ActivityRepeatInstanceModel> Repeats { get; private set; }
    }
}