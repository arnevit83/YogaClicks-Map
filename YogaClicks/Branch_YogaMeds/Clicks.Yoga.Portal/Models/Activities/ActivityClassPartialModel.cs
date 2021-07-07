using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivityClassPartialModel
    {
        public ActivityClassPartialModel(ActivityRepeatInstanceModel repeat, bool editable)
        {
            Repeat = repeat;
            Editable = editable;
        }

        public ActivityRepeatInstanceModel Repeat { get; private set; }

        public bool Editable { get; private set; }
    }
}