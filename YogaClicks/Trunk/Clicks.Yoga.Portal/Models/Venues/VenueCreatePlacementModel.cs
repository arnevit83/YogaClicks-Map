using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.Venues
{
    public class VenueCreatePlacementModel
    {
        public VenueCreatePlacementModel()
        {
            Teacher = new TeacherSelectorModel();
        }

        public TeacherSelectorModel Teacher { get; private set; }
    }
}