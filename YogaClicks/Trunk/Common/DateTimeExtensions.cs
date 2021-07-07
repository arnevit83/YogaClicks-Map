using System;
using System.Globalization;

namespace Common
{
    public static class DateTimeExtensions
    {
        public static DateTime YearBegin(this DateTime time)
        {
            return new DateTime(time.Year, 1, 1);
        }

        public static DateTime YearEnd(this DateTime time)
        {
            return time.YearBegin().AddYears(1);
        }

        public static DateTime MonthBegin(this DateTime time)
        {
            return new DateTime(time.Year, time.Month, 1);
        }

        public static DateTime MonthEnd(this DateTime time)
        {
            return time.MonthBegin().AddMonths(1);
        }

        public static DateTime WeekBegin(this DateTime time, DayOfWeek firstDay)
        {
            while (time.DayOfWeek != firstDay)
            {
                time = time.AddDays(-1);
            }

            return time.Date;
        }

        public static DateTime WeekEnd(this DateTime time, DayOfWeek firstDay)
        {
            return time.WeekBegin(firstDay).AddDays(7);
        }

        public static DateTime DayBegin(this DateTime time)
        {
            return time.Date;
        }

        public static DateTime DayEnd(this DateTime time)
        {
            return time.DayBegin().AddDays(1);
        }
    }
}