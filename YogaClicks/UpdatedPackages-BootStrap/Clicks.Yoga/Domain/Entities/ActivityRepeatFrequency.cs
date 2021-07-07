using System;

namespace Clicks.Yoga.Domain.Entities
{
    public class ActivityRepeatFrequency : Entity
    {
        public string Name { get; set; }

        public bool IsSingle { get; set; }

        public bool IsDaily { get; set; }

        public bool IsWeekly { get; set; }

        public bool IsFortnightly { get; set; }

        public DateTime? NextRepeatTime(DateTime startTime, DateTime date)
        {
            if (date.Date != date)
                throw new ArgumentException("Date must not have a time component.", "date");

            if (IsDaily)
            {
                if (date.Add(startTime.TimeOfDay) < DateTime.Now) date = date.AddDays(1);

                return new DateTime(date.Year, date.Month, date.Day, startTime.Hour, startTime.Minute, startTime.Second);
            }
            else if (IsWeekly || IsFortnightly)
            {
                while (date.DayOfWeek != startTime.DayOfWeek || date.Add(startTime.TimeOfDay) < DateTime.Now)
                {
                    date = date.AddDays(1);
                }

                if (IsFortnightly && Convert.ToInt32((date - startTime.Date).TotalDays % 14) != 0 || date.Add(startTime.TimeOfDay) < DateTime.Now)
                {
                    date = date.AddDays(7);
                }

                return new DateTime(date.Year, date.Month, date.Day, startTime.Hour, startTime.Minute, startTime.Second);
            }
            else
            {
                return null;
            }
        }
    }
}