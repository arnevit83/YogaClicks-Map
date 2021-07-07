using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Domain.Services.Interfaces;
using Clicks.Yoga.Portal.Models.Activities;
using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.SignUp
{
    [DataContract]
    public class ActivityCreateActivityViewModel
    {
        public ActivityCreateActivityViewModel()
        {
            AbilityLevel = new AbilityLevelSelectorModel();
            Date = new DateEditorModel(DateTime.Now.Date) { Minimum = DateTime.Now, MaximumYear = DateTime.Now.Year + 10 };
            TimeRange = new TimeRangeEditorModel();
            Styles = new SignUpStyleChooserModel();
            Conditions = new SignUpConditionChooserModel();
            Teachers = new ActivityTeacherChooserModel();
            Venue = new SignUpVenueSelectorCreatorModel();
        }

        public ActivityCreateClassModel CreateClassModel { get; set; }

        public SignUpActivityTypeSelectorModel Type { get; private set; }

        public EntityHandleSelectorModel PromoterHandle { get; private set; }
        
        [DataMember]
        public int HiddenType { get; set; }
        
        [DataMember]
        public int HiddenHandle { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public AbilityLevelSelectorModel AbilityLevel { get; private set; }

        [DataMember]
        public DateEditorModel Date { get; private set; }

        [DataMember]
        public SignUpVenueSelectorCreatorModel Venue { get; private set; }

        [DataMember]
        public TimeRangeEditorModel TimeRange { get; private set; }


        [DataMember]
        public bool Private { get; set; }

        [DataMember]
        public bool BookingRequired { get; set; }

        [DataMember]
        public string Price { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public SignUpStyleChooserModel Styles { get; private set; }

        [DataMember]
        public SignUpConditionChooserModel Conditions { get; private set; }
        
        [DataMember]
        public ActivityTeacherChooserModel Teachers { get; private set; }


        public void PopulateEntity(Activity activity, IActivityService activityService, IStyleService styleService, IMedicService medicService, ITeacherService teacherService, IVenueService venueService, IEntityService entityService, ISecurityContext securityContext)
        {
            var promoterHandle = entityService.GetEntityHandle(HiddenHandle);
            var profile = entityService.GetEntity<IProfileBanner>(promoterHandle);


            if (!securityContext.CanUpdate(promoterHandle))
                throw new PermissionDeniedException();


            activity.Name = Name;
            activity.Public = true;
            activity.AbilityLevel = activityService.GetAbilityLevel(AbilityLevel.Id);
            activity.StartTime = Date.DateTime.Value.Add(TimeRange.StartTimeSpan.Value);
            activity.FinishTime = Date.DateTime.Value.Add(TimeRange.FinishTimeSpan.Value); activity.BookingRequired = BookingRequired;
            activity.Price = Price ?? "";
            activity.Description = Description ?? "";
            activity.Public = !Private; 
            Styles.PopulateCollection(activity.Styles, styleService);
            Conditions.PopulateCollection(activity.Conditions, medicService);
            Teachers.PopulateEntity(activity, activityService, teacherService);
            if (Venue.Id.HasValue)
            {
                activityService.AddActivityVenue(activity, venueService.GetVenue(Venue.Id.Value));
            }
            activity.PromoterHandle = promoterHandle;
            activity.Type = activityService.GetActivityType(HiddenType);
            activity.Image = promoterHandle.Image;
            activity.ProfileBanner = profile != null ? profile.ProfileBanner : null;
        }

        public ActivityCreateActivityViewModel PopulateMetadata(IActivityService activityService, IStyleService styleService, IMedicService medicService, IEnumerable<Country> countries, EntityHandleSelectorModel promoterHandle, SignUpActivityTypeSelectorModel type)
        {
            AbilityLevel.PopulateMetadata(activityService.GetAbilityLevels());
            Styles.PopulateMetadata(styleService.GetStyles());
            Conditions.PopulateMetadata(medicService.GetConditions());
            Venue.PopulateMetadata(countries);
            PromoterHandle = promoterHandle;
            Type = type;
            return this;
        }
    }
}