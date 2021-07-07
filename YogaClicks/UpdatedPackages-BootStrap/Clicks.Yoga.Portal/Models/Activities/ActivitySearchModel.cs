using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivitySearchModel
    {
        public ActivitySearchModel(ActivitySearchResponse response, ActivitySearchCriteriaModel criteria, ActivityType type)
        {
            Response = new ActivitySearchResponseModel(response);
            Criteria = criteria;

            if (type != null)
            {
                Type = new ActivityTypeModel(type);
            }
        }

        public ActivityTypeModel Type { get; private set; }

        public ActivitySearchResponseModel Response { get; private set; }

        public ActivitySearchCriteriaModel Criteria { get; private set; }

        public int NextPageIndex
        {
            get { return Criteria.PageIndex + 1; }
        }

        public int PreviousPageIndex
        {
            get { return Criteria.PageIndex - 1; }
        }
    }
}