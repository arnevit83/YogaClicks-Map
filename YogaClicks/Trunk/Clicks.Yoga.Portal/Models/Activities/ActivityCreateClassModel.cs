using System.Runtime.Serialization;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Domain.Services.Interfaces;

namespace Clicks.Yoga.Portal.Models.Activities
{
    [DataContract]
    public class ActivityCreateClassModel
    {
        [DataMember]
        public ActivityCreateStep1Model Step1Model { get; set; }

        [DataMember]
        public ActivityCreateClassStep2Model Step2Model { get; set; }

        [DataMember]
        public ActivityCreateClassStep3Model Step3Model { get; set; }

        [DataMember]
        public ActivityCreateClassStep4Model Step4Model { get; set; }

        [DataMember]
        public ActivityCreateClassStep5Model Step5Model { get; set; }

        public void PopulateEntity(
            Activity entity,
            ISecurityContext securityContext,
            IActivityService activityService,
            IEntityService entityService,
            IStyleService styleService,
            ITeacherService teacherService,
            IVenueService venueService,
            IMedicService medicService)
        {
            Step1Model.PopulateEntity(entity, securityContext, activityService, entityService);
            Step2Model.PopulateEntity(entity, activityService);
            Step3Model.PopulateEntity(entity, styleService, medicService);
            Step4Model.PopulateEntity(entity, activityService, teacherService);
            Step5Model.PopulateEntity(entity, activityService, venueService);
        }
    }
}