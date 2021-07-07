using System;

namespace Clicks.Yoga.Domain.Entities
{
    public class NotificationPreferences : Entity
    {
        public virtual User User { get; set; }

        public bool EmailDigestEnabled { get; set; }

        public virtual Timescale EmailDigestTimescale { get; set; }

        public DateTime? NextDigestEmailTime { get; set; }

        public void CalculateNextDigestEmailTime()
        {
            if (NextDigestEmailTime > DateTime.Now) return;

            var time = NextDigestEmailTime ?? DateTime.Now;

            if (EmailDigestTimescale.IsDay)
                time = time.AddDays(1);
            else if (EmailDigestTimescale.IsWeek)
                time = time.AddDays(7);
            else if (EmailDigestTimescale.IsMonth)
                time = time.AddMonths(1);

            NextDigestEmailTime = time;
        }

        public bool DigestEmailDue
        {
            get { return NextDigestEmailTime < DateTime.Now && NextDigestEmailTime >= DateTime.Now.AddDays(-1); }
        }
    }
}