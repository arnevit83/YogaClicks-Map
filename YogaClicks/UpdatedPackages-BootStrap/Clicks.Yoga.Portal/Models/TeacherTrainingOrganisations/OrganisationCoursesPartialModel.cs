using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.TeacherTrainingOrganisations
{
    public class OrganisationCoursesPartialModel
    {
        public OrganisationCoursesPartialModel(IEnumerable<TeacherTrainingCourse> courses)
        {
            Courses = new List<TeacherTrainingCourseModel>();

            foreach (var course in courses)
            {
                Courses.Add(new TeacherTrainingCourseModel(course));
            }
        }

        public IList<TeacherTrainingCourseModel> Courses { get; private set; }
    }
}