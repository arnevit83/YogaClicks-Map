
namespace Clicks.Yoga.Domain.Entities
{
   public class TeacherTrainingOrganisationFaculty : Entity
    {
       public TeacherTrainingOrganisationFaculty()
       {
     

       }
        public TeacherTrainingOrganisationFaculty(TeacherTrainingOrganisation tto, Teacher teacher)
        {
            Faculty = tto;
            FacultyMember = teacher;
      
        }

        public virtual TeacherTrainingOrganisation Faculty { get; set; }

        public virtual Teacher FacultyMember { get; set; }



    }
}
