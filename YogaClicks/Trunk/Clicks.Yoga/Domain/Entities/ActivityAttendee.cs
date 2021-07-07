namespace Clicks.Yoga.Domain.Entities
{
    public class ActivityAttendee : Entity, INewsLink
    {
        public virtual Activity Activity { get; set; }

        public virtual EntityHandle Handle { get; set; }

        public bool? Confirmed { get; set; }

        public bool IsConfirmed
        {
            get { return Confirmed.HasValue && Confirmed.Value; }
        }

        public bool IsRejected
        {
            get { return Confirmed.HasValue && !Confirmed.Value; }
        }

        public bool IsPending
        {
            get { return !Confirmed.HasValue; }
        }

        public ActivityAttendanceStatus GetAttendanceStatus()
        {
            if (IsConfirmed)
                return ActivityAttendanceStatus.Confirmed;

            if (IsRejected)
                return ActivityAttendanceStatus.Rejected;

            return ActivityAttendanceStatus.Unconfirmed;
        }

        bool INewsLink.NewsRequired
        {
            get { return IsConfirmed; }
        }

        Profile INewsLink.NewsSubscriber
        {
            get { return Handle.Owner.Profile; }
        }

        IEntityHandle INewsLink.NewsSubject
        {
            get { return Activity; }
        }
    }
}