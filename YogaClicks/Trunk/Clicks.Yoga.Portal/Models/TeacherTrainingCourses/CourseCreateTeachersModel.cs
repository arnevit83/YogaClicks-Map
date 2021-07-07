using System.Runtime.Serialization;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.TeacherTrainingCourses
{
    [DataContract]
    public class CourseCreateTeachersModel
    {
        public CourseCreateTeachersModel()
        {
            Teachers = new TeacherTrainingCourseTeacherChooserModel();
        }

        [DataMember]
        public TeacherTrainingCourseTeacherChooserModel Teachers { get; private set; }
        
        public bool Back { get; set; }

        public void PopulateEntity(TeacherTrainingCourse entity, ITeacherTrainingService teacherTrainingService, ITeacherService teacherService)
        {
            Teachers.PopulateEntity(entity, teacherTrainingService, teacherService);
        }
    }
}