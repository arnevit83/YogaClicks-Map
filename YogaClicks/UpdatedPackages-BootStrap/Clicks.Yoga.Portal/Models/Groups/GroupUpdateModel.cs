using Clicks.Yoga.Context;
using Clicks.Yoga.Domain;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Groups
{
    public class GroupUpdateModel
    {
        public GroupUpdateModel()
        {
            PromoterHandle = new EntityHandleSelectorModel();
            Image = new ProfileImageEditorModel();
            ProfileBanner = new ProfileBannerEditorModel();
            Professions = new EntityTypeChooserModel();
            Styles = new StyleChooserModel();
        }

        public GroupUpdateModel(Group group)
        {
            Name = group.Name;
            Description = group.Description;
            PromoterHandle = new EntityHandleSelectorModel(group.PromoterHandle);
            Image = new ProfileImageEditorModel(group.Image);
            ProfileBanner = new ProfileBannerEditorModel(group.ProfileBanner);
            Professional = group.Professional;
            Professions = new EntityTypeChooserModel(group.Professions);
            Styles = new StyleChooserModel(group.Styles);
        }

        public GroupModel Group { get; private set; }

        public GroupPermissions Permissions { get; private set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public EntityHandleSelectorModel PromoterHandle { get; private set; }

        public ProfileImageEditorModel Image { get; private set; }

        public ProfileBannerEditorModel ProfileBanner { get; private set; }

        public bool Professional { get; set; }

        public EntityTypeChooserModel Professions { get; private set; }

        public StyleChooserModel Styles { get; private set; }

        public void PopulateEntity(
            Group group, 
            ISecurityContext securityContext,
            IEntityService entityService,
            IImageService imageService,
            IStyleService styleService)
        {
            var promoterHandle = entityService.GetEntityHandle(PromoterHandle.Id);

            if (!securityContext.CanUpdate(promoterHandle))
                throw new PermissionDeniedException();

            group.Name = Name;
            group.Description = Description;
            group.PromoterHandle = promoterHandle;
            group.Image = Image.PopulateEntity(() => group.Image, imageService);
            group.ProfileBanner = ProfileBanner.PopulateEntity(() => group.ProfileBanner, imageService);
            group.Professional = Professional;
            group.ProfessionsRestricted = Professions.Ids.Count > 0;
            Professions.PopulateCollection(group.Professions, entityService);
            Styles.PopulateCollection(group.Styles, styleService);
        }

        public GroupUpdateModel PopuplateMetadata(
            Group group,
            GroupPermissions permissions,
            IProfileService profileService,
            IStyleService styleService)
        {
            Group = new GroupModel(group);
            Permissions = permissions;
            PromoterHandle.PopulateMetadata(profileService.GetProfessionalEntitiesAndProfile());
            Styles.PopulateMetadata(styleService.GetStyles());
            Professions.PopulateMetadata(profileService.GetProfessionalEntityTypes());
            return this;
        }
    }
}