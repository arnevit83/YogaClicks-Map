using System.Runtime.Serialization;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.Groups
{
    [DataContract]
    public class GroupCreateStep1Model
    {
        public GroupCreateStep1Model()
        {
            PromoterHandle = new EntityHandleSelectorModel();
        }

        public GroupCreateStep1Model(int ownerId)
        {
            OwnerId = ownerId;
            PromoterHandle = new EntityHandleSelectorModel();
        }

        [DataMember]
        public int OwnerId { get; set; }

        [DataMember]
        public EntityHandleSelectorModel PromoterHandle { get; private set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public bool Private { get; set; }

        public void PopulateEntity(
            Group entity,
            ISecurityContext securityContext,
            IEntityService entityService)
        {
            var promoterHandle = entityService.GetEntityHandle(PromoterHandle.Id);
            var profile = entityService.GetEntity<IProfileBanner>(promoterHandle);

            if (!securityContext.CanUpdate(promoterHandle))
                throw new PermissionDeniedException();

            entity.Name = Name;
            entity.Description = Description;
            entity.PromoterHandle = promoterHandle;
            entity.Image = promoterHandle.Image;
            entity.ProfileBanner = profile != null ? profile.ProfileBanner : null;
            entity.Public = !Private;
        }

        public GroupCreateStep1Model PopulateMetadata(IProfileService profileService)
        {
            PromoterHandle.PopulateMetadata(profileService.GetProfessionalEntitiesAndProfile());

            return this;
        }
    }
}