using System.Runtime.Serialization;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.Activities
{
    [DataContract]
    public class ActivityCreateStep3Model
    {
        public ActivityCreateStep3Model()
        {
            Teachers = new ActivityTeacherChooserModel();
        }

        [DataMember]
        public ActivityTeacherChooserModel Teachers { get; private set; }

        public bool Back { get; set; }

        public void PopulateEntity(Activity entity, IActivityService activityService, ITeacherService teacherService)
        {
            Teachers.PopulateEntity(entity, activityService, teacherService);
        }

        public ActivityCreateStep3Model PopulateMetadata(ITeacherService teacherService)
        {
            Teachers.PopulateMetadata(teacherService);
            return this;
        }
    }
}