using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;

namespace Clicks.Yoga.Portal.Models.Editors
{
    public class TeacherTrainingCourseTeacherChooserModel : TeacherChooserModel
    {
        public TeacherTrainingCourseTeacherChooserModel() {}

        public TeacherTrainingCourseTeacherChooserModel(ICollection<Teacher> entities) : base(entities) {}

        public void PopulateEntity(TeacherTrainingCourse course, ITeacherTrainingService teacherTrainingService, ITeacherService teacherService)
        {
            foreach (var element in course.Teachers.ToList())
            {
                if (!Ids.Contains(element.Teacher.Id))
                {
                    teacherTrainingService.RemoveCourseTeacher(element.Course, element.Teacher);
                }
            }

            foreach (var id in Ids)
            {
                if (!course.Teachers.Any(e => e.Teacher.Id == id))
                {
                    teacherTrainingService.AddCourseTeacher(course, teacherService.GetTeacher(id));
                }
            }
        }
    }
}