using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.TeacherTrainingOrganisations
{
    public class OrganisationCoursesModel
    {
        public OrganisationCoursesModel(TeacherTrainingOrganisation organisation)
        {
            Organisation = new TeacherTrainingOrganisationModel(organisation);
        }

        public TeacherTrainingOrganisationModel Organisation { get; set; }
    }
}