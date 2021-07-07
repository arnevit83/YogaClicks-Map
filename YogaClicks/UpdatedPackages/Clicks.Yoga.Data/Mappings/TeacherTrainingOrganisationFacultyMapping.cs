using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{

    public class TeacherTrainingOrganisationFacultyMapping : EntityMapping<TeacherTrainingOrganisationFaculty>
    {


     public TeacherTrainingOrganisationFacultyMapping()
        {
            HasRequired(e => e.Faculty).WithMany(e => e.Faculty).Map(a => a.MapKey("TTOId"));
            HasRequired(e => e.FacultyMember).WithMany(e => e.FacultyMember).Map(a => a.MapKey("TeacherId"));
    } 
    }
}

