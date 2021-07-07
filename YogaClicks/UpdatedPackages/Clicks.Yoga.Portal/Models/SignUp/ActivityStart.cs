using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Activities;
using Clicks.Yoga.Portal.Models.Editors;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.SignUp
{
    public class ActivityStart
    {
        public ActivityStart()
        {
            Type = new SignUpActivityTypeSelectorModel();
            AbilityLevel = new AbilityLevelSelectorModel();
            PromoterHandle = new ActivitiesEntityHandleSelectorModel();
        }

        public ActivityStart(int ownerId, Profile profile, Teacher teacher, Venue venue, TeacherTrainingOrganisation tto)
            : this()
        {
            OwnerId = ownerId; 
            
            Profile = new ProfileModel(profile);

            if (teacher != null)
            {
                Teacher = new TeacherModel(teacher);
            }

            if (venue != null)
            {
                Venue = new VenueModel(venue);
            }

            if (tto != null)
            {
                TTO = new TeacherTrainingOrganisationModel(tto);
            }
        }
        [DataMember]
        public int ActivityCloneId { get; set; }

        [DataMember]
        public int OwnerId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public SignUpActivityTypeSelectorModel Type { get; private set; }

        [DataMember]
        public AbilityLevelSelectorModel AbilityLevel { get; private set; }

        [DataMember]
        public ActivitiesEntityHandleSelectorModel PromoterHandle { get; private set; }

        [DataMember]
        public bool Private { get; set; }

        
        public ProfileModel Profile { get; set; }

        public TeacherModel Teacher { get; set; }

        public VenueModel Venue { get; set; }

        public TeacherTrainingOrganisationModel TTO { get; set; }

        public bool HasTeacher
        {
            get { return Teacher != null; }
        }

        public bool HasVenue
        {
            get { return Venue != null; }
        }

        public bool HasTTO
        {
            get { return TTO != null; }
        }

        public void PopulateEntity(Activity entity, ISecurityContext securityContext, IActivityService activityService, IEntityService entityService)
        {
            var promoterHandle = entityService.GetEntityHandle(PromoterHandle.Id);
            var profile = entityService.GetEntity<IProfileBanner>(promoterHandle);

            if (!securityContext.CanUpdate(promoterHandle))
                throw new PermissionDeniedException();

            entity.Name = Name;
            entity.Description = "";
            entity.Type = activityService.GetActivityType(Type.Id);
            entity.AbilityLevel = activityService.GetAbilityLevel(AbilityLevel.Id);
            entity.PromoterHandle = promoterHandle;
            entity.Public = !Private;
            entity.Image = promoterHandle.Image;
            entity.ProfileBanner = profile != null ? profile.ProfileBanner : null;
        }

        public ActivityStart PopulateMetadata(IActivityService activityService, IProfileService profileService)
        {
            Type.PopulateMetadata(activityService.GetActivityTypes());

            AbilityLevel.PopulateMetadata(activityService.GetAbilityLevels());
            PromoterHandle.PopulateMetadata(profileService.GetProfessionalEntities());

            return this;
        }
    }
}