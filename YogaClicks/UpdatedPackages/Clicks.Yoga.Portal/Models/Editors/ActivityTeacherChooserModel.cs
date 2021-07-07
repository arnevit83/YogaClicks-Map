using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;

namespace Clicks.Yoga.Portal.Models.Editors
{
    [DataContract]
    public class ActivityTeacherChooserModel : TeacherChooserModel
    {
        private bool p;

        public ActivityTeacherChooserModel() {}

        public ActivityTeacherChooserModel(ICollection<Teacher> entities) : base(entities) {}

        public ActivityTeacherChooserModel(bool p)
        {
            // TODO: Complete member initialization
            this.p = p;
        }

        public void PopulateEntity(Activity activity, IActivityService activityService, ITeacherService teacherService)
        {
            foreach (var element in activity.Teachers.ToList())
            {
                if (!Ids.Contains(element.Teacher.Id))
                {
                    activityService.RemoveActivityTeacher(element.Activity, element.Teacher);
                }
            }

            foreach (var id in Ids)
            {
                if (!activity.Teachers.Any(e => e.Teacher.Id == id))
                {
                    activityService.AddActivityTeacher(activity, teacherService.GetTeacher(id));
                }
            }
        }

        public void PopulateMetadata(ITeacherService teacherService)
        {
            PopulateMetadata(Ids.Select(teacherService.GetTeacher));
        }
    }
}