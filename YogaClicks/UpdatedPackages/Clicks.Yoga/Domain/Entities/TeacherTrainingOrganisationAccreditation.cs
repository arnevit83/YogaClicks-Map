namespace Clicks.Yoga.Domain.Entities
{
    public class TeacherTrainingOrganisationAccreditation : Entity
    {
        public TeacherTrainingOrganisationAccreditation()
        {
        }

        public TeacherTrainingOrganisationAccreditation(AccreditationBody body)
        {
            AccreditationBody = body;
        }

        public int TeacherTrainingOrganisationId { get; set; }

        public int AccreditationBodyId { get; set; }

        public virtual TeacherTrainingOrganisation TeacherTrainingOrganisation { get; set; }

        public virtual AccreditationBody AccreditationBody { get; set; }
    }
}