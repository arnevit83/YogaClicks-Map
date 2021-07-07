using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.TeacherTrainingOrganisations
{
    public class OrganisationCreatePlacementModel
    {
        public OrganisationCreatePlacementModel()
        {
            Teacher = new TeacherSelectorModel();
        }

        public TeacherSelectorModel Teacher { get; private set; }
    }
}