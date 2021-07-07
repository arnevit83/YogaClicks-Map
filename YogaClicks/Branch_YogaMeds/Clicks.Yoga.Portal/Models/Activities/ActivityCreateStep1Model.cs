using System.Runtime.Serialization;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.Activities
{
    [DataContract]
    public class ActivityCreateStep1Model
    {
        public ActivityCreateStep1Model()
        {
            Type = new ActivityTypeSelectorModel();
            AbilityLevel = new AbilityLevelSelectorModel();
            PromoterHandle = new EntityHandleSelectorModel();
        }

        public ActivityCreateStep1Model(int ownerId) : this()
        {
            OwnerId = ownerId;
        }

        [DataMember]
        public int OwnerId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public ActivityTypeSelectorModel Type { get; private set; }

        [DataMember]
        public AbilityLevelSelectorModel AbilityLevel { get; private set; }

        [DataMember]
        public EntityHandleSelectorModel PromoterHandle { get; private set; }

        [DataMember]
        public bool Private { get; set; }

        public void PopulateEntity(
            Activity entity,
            ISecurityContext securityContext,
            IActivityService activityService,
            IEntityService entityService)
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

        public ActivityCreateStep1Model PopulateMetadata(
            IActivityService activityService,
            IProfileService profileService)
        {
            Type.PopulateMetadata(activityService.GetActivityTypes());

            AbilityLevel.PopulateMetadata(activityService.GetAbilityLevels());
            PromoterHandle.PopulateMetadata(profileService.GetProfessionalEntities());

            return this;
        }
    }
}