using System;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using Clicks.Yoga.Portal.Models.Entities;
using Clicks.Yoga.Domain.Services.Interfaces;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivityUpdateClassModel
    {
        public ActivityUpdateClassModel()
        {
            Date = new DateEditorModel();
            TimeRange = new TimeRangeEditorModel();
            RepeatFrequency = new ActivityRepeatFrequencySelectorModel();
            AbilityLevel = new AbilityLevelSelectorModel();
            PromoterHandle = new EntityHandleSelectorModel();
            Styles = new StyleChooserModel();
            Conditions = new ConditionChooserModel();
        }

        public ActivityUpdateClassModel(Activity activity)
        {
            Name = activity.Name;
            Description = activity.Description;
            BookingRequired = activity.BookingRequired;
            Price = activity.Price;
            Date = new DateEditorModel(activity.StartTime);
            TimeRange = new TimeRangeEditorModel(activity.StartTime, activity.FinishTime);
            Repeat = !activity.RepeatFrequency.IsSingle;
            RepeatFrequency = new ActivityRepeatFrequencySelectorModel(activity.RepeatFrequency);
            AbilityLevel = new AbilityLevelSelectorModel(activity.AbilityLevel);
            PromoterHandle = new EntityHandleSelectorModel(activity.PromoterHandle);
            Styles = new StyleChooserModel(activity.Styles);
            Conditions = new ConditionChooserModel(activity.Conditions);
        }

        public ActivityModel Activity { get; private set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool BookingRequired { get; set; }

        public string Price { get; set; }

        public DateEditorModel Date { get; private set; }

        public TimeRangeEditorModel TimeRange { get; private set; }

        public bool Repeat { get; set; }

        public ActivityRepeatFrequencySelectorModel RepeatFrequency { get; private set; }

        public AbilityLevelSelectorModel AbilityLevel { get; private set; }

        public EntityHandleSelectorModel PromoterHandle { get; private set; }

        public StyleChooserModel Styles { get; private set; }

        public ConditionChooserModel Conditions { get; private set; }

        public void PopulateEntity(
            Activity activity,
            ISecurityContext securityContext,
            IActivityService activityService,
            IEntityService entityService,
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
            activity.StartTime = Date.DateTime.Value.Add(TimeRange.StartTimeSpan.Value);
            activity.FinishTime = Date.DateTime.Value.Add(TimeRange.FinishTimeSpan.Value);
            activity.RepeatFrequency = Repeat ? activityService.GetRepeatFrequency(RepeatFrequency.Id) : activityService.GetSingleRepeatFrequency();
            activity.AbilityLevel = activityService.GetAbilityLevel(AbilityLevel.Id);
            activity.PromoterHandle = promoterHandle;

            Styles.PopulateCollection(activity.Styles, styleService);
            Conditions.PopulateCollection(activity.Conditions, medicService);
        }

        public ActivityUpdateClassModel PopulateMetadata(
            Activity activity,
            IActivityService activityService,
            IProfileService profileService,
            IStyleService styleService,
            IMedicService medicService)
        {
            Activity = new ActivityModel(activity);

            Date.Minimum = DateTime.Now.Date;
            Date.MaximumYear = DateTime.Now.Year + 10;

            AbilityLevel.PopulateMetadata(activityService.GetAbilityLevels());
            RepeatFrequency.PopulateMetadata(activityService.GetRepeatFrequencies());
            PromoterHandle.PopulateMetadata(profileService.GetProfessionalEntitiesAndProfile());
            Styles.PopulateMetadata(styleService.GetStyles());
            Conditions.PopulateMetadata(medicService.GetConditions());

            return this;
        }
    }
}