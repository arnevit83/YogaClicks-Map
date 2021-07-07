using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivitySearchResponseModel
    {
        public ActivitySearchResponseModel(ActivitySearchResponse response)
        {
            Activities = new List<ActivityModel>();
            HasNextPage = response.HasNextPage;
            HasPreviousPage = response.HasPreviousPage;

            foreach (var activity in response.Activities)
            {
                Activities.Add(new ActivityModel(activity));
            }
        }

        public IList<ActivityModel> Activities { get; private set; }

        public bool HasNextPage { get; private set; }

        public bool HasPreviousPage { get; private set; }
    }
}