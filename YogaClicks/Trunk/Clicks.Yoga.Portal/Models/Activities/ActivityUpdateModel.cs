using System;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Context;
using Clicks.Yoga.Portal.Models.Editors;
using Clicks.Yoga.Portal.Models.Entities;
using Microsoft.Ajax.Utilities;
using Clicks.Yoga.Domain.Services.Interfaces;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivityUpdateModel
    {
        public ActivityUpdateModel()
        {
            StartTime = new DateTimeEditorModel();
            FinishTime = new DateTimeEditorModel();
            Type = new ActivityTypeSelectorModel();
            AbilityLevel = new AbilityLevelSelectorModel();
            PromoterHandle = new EntityHandleSelectorModel();
            Image = new ProfileImageEditorModel();
            ProfileBanner = new ProfileBannerEditorModel();
            Styles = new StyleChooserModel();
            Conditions = new ConditionChooserModel();
        }

        public ActivityUpdateModel(Activity activity)
        {
            Name = activity.Name;
            Description = activity.Description;
            BookingRequired = activity.BookingRequired;
            Price = activity.Price;
            StartTime = new DateTimeEditorModel(activity.StartTime);
            FinishTime = new DateTimeEditorModel(activity.FinishTime);
            Type = new ActivityTypeSelectorModel(activity.Type);
            AbilityLevel = new AbilityLevelSelectorModel(activity.AbilityLevel);
            PromoterHandle = new EntityHandleSelectorModel(activity.PromoterHandle);
            Image = new ProfileImageEditorModel(activity.Image);
            ProfileBanner = new ProfileBannerEditorModel(activity.ProfileBanner);
            Styles = new StyleChooserModel(activity.Styles);
            Conditions = new ConditionChooserModel(activity.Conditions);
        }

        public ActivityModel Activity { get; private set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool BookingRequired { get; set; }

        public string Price { get; set; }

        public DateTimeEditorModel StartTime { get; private set; }

        public DateTimeEditorModel FinishTime { get; private set; }

        public ActivityTypeSelectorModel Type { get; private set; }

        public AbilityLevelSelectorModel AbilityLevel { get; private set; }

        public EntityHandleSelectorModel PromoterHandle { get; private set; }

        public ProfileImageEditorModel Image { get; private set; }

        public ProfileBannerEditorModel ProfileBanner { get; private set; }

        public StyleChooserModel Styles { get; private set; }

        public ConditionChooserModel Conditions { get; private set; }

        public void PopulateEntity(
            Activity activity,
            ISecurityContext securityContext,
            IActivityService activityService,
            IEntityService entityService,
            IImageService imageService,
            IStyleService styleService,
            IMedicService medicService)
        {
            var promoterHandle = entityService.GetEntityHandle(PromoterHandle.Id);

            if (!securityContext.CanUpdate(promoterHandle))
                throw new PermissionDeniedException();

            activity.Name = Name;
            activity.Description = Description ?? "";
            activity.BookingRequired = BookingRequired;
            activity.Price = Price ?? "";
            activity.StartTime = StartTime.DateTime.Value;
            activity.FinishTime = FinishTime.DateTime.Value;
            activity.Type = activityService.GetActivityType(Type.Id);
            activity.AbilityLevel = activityService.GetAbilityLevel(AbilityLevel.Id);
            activity.PromoterHandle = promoterHandle;
            activity.Image = Image.PopulateEntity(() => activity.Image, imageService);
            activity.ProfileBanner = ProfileBanner.PopulateEntity(() => activity.ProfileBanner, imageService);

            Styles.PopulateCollection(activity.Styles, styleService);
            Conditions.PopulateCollection(activity.Conditions, medicService);
        }

        public ActivityUpdateModel PopulateMetadata(
            Activity activity,
            IActivityService activityService,
            IProfileService profileService,
            IStyleService styleService,
            IMedicService medicService)
        {
            Activity = new ActivityModel(activity);

            StartTime.Minimum = FinishTime.Minimum = DateTime.Now.Date;
            StartTime.MaximumYear = FinishTime.MaximumYear = DateTime.Now.Year + 10;

            Type.PopulateMetadata(activityService.GetActivityTypes());
            AbilityLevel.PopulateMetadata(activityService.GetAbilityLevels());
            PromoterHandle.PopulateMetadata(profileService.GetProfessionalEntitiesAndProfile());
            Styles.PopulateMetadata(styleService.GetStyles());
            Conditions.PopulateMetadata(medicService.GetConditions());

            return this;
        }
    }
}