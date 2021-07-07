using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivityEditableListPartialModel
    {
        public ActivityEditableListPartialModel(string action, object routeValues)
        {
            Action = action;
            RouteValues = routeValues;
        }

        public string Action { get; private set; }

        public object RouteValues { get; private set; }
    }
}