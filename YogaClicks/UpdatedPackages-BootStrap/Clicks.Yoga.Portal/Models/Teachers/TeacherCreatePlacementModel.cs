using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.Teachers
{
    public class TeacherCreatePlacementModel
    {
        public TeacherCreatePlacementModel()
        {
            Venue = new VenueSelectorModel();
        }

        public VenueSelectorModel Venue { get; private set; }
    }
}