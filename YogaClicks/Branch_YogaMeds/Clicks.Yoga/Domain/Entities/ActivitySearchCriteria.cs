using System;
using Clicks.Yoga.Geography;

namespace Clicks.Yoga.Domain.Entities
{
    public class ActivitySearchCriteria
    {
        public int? OwnerId { get; set; }

        public int? TypeId { get; set; }

        public GeographicPoint Coordinates { get; set; }

        public int? Radius { get; set; }

        public int? VenueId { get; set; }

        public int? TTOEntityId { get; set; }

        public string VenueName { get; set; }

        public int? TeacherId { get; set; }

        public string TeacherName { get; set; }

        public EntityReference? Promoter { get; set; }

        public EntityReference? Attendee { get; set; }

        public string Keywords { get; set; }

        public int? AbilityLevelId { get; set; }

        public int? StyleId { get; set; }

        public int? ConditionId { get; set; }

        public DateTime? EarliestTime { get; set; }

        public DateTime? LatestTime { get; set; }

        public DayOfWeek? DayOfWeek { get; set; }

        public ActivityTimeOfDay? TimeOfDay { get; set; }

        public TimeSpan? Duration { get; set; }

        public ActivitySortOrder SortOrder { get; set; }

        public int PageIndex { get; set; }

        public int? PageSize { get; set; }
    }

    public enum ActivitySortOrder
    {
        Date,
        Distance
    }
}