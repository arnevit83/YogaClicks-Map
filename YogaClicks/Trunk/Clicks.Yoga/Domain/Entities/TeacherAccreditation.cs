namespace Clicks.Yoga.Domain.Entities
{
    public class TeacherAccreditation : Entity
    {
        public TeacherAccreditation()
        {
        }

        public TeacherAccreditation(AccreditationBody body)
        {
            AccreditationBody = body;
        }

        public int TeacherId { get; set; }

        public int AccreditationBodyId { get; set; }

        public virtual Teacher Teacher { get; set; }

        public virtual AccreditationBody AccreditationBody { get; set; }
    }
}