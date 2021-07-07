using System.Runtime.Serialization;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;

namespace Clicks.Yoga.Portal.Models.Activities
{
    [DataContract]
    public class ActivityCreateModel
    {
        [DataMember]
        public string ActivityTypeName { get; set; }

        [DataMember]
        public ActivityCreateStep1Model Step1Model { get; set; }

        [DataMember]
        public ActivityCreateStep2Model Step2Model { get; set; }

        [DataMember]
        public ActivityCreateStep3Model Step3Model { get; set; }

        [DataMember]
        public ActivityCreateStep4Model Step4Model { get; set; }

        public void PopulateEntity(
            Activity entity,
            ISecurityContext securityContext,
            IActivityService activityService,
            IEntityService entityService,
            ITeacherService teacherService,
            IVenueService venueService,
            IStyleService styleService)
        {
            Step1Model.PopulateEntity(entity, securityContext, activityService, entityService);
            Step2Model.PopulateEntity(entity, styleService);
            Step3Model.PopulateEntity(entity, activityService, teacherService);
            Step4Model.PopulateEntity(entity, activityService, venueService);
        }
    }
}