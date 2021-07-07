using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class TeacherTrainingCourseSearchCriteriaModel
    {
        public int OrganisationId { get; set; }

        public bool? Completed { get; set; }

        public void PopulateEntity(TeacherTrainingCourseSearchCriteria entity)
        {
            entity.OrganisationId = OrganisationId;
            entity.Completed = Completed;
        }
    }
}