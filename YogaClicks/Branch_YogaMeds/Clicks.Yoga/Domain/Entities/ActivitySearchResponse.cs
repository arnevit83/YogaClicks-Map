using System.Collections.Generic;

namespace Clicks.Yoga.Domain.Entities
{
    public class ActivitySearchResponse
    {
        public ActivitySearchResponse(IEnumerable<Activity> activities, bool hasNextPage, bool hasPreviousPage)
        {
            Activities = new List<Activity>();
            HasNextPage = hasNextPage;
            HasPreviousPage = hasPreviousPage;

            foreach (var activity in activities)
            {
                Activities.Add(activity);
            }
        }

        public IList<Activity> Activities { get; private set; }

        public bool HasNextPage { get; private set; }

        public bool HasPreviousPage { get; private set; }
    }
}