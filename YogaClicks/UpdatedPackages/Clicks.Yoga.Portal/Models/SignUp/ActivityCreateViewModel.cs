using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Domain.Services.Interfaces;
using Clicks.Yoga.Portal.Models.Activities;
using Clicks.Yoga.Portal.Models.Editors;
using Clicks.Yoga.Portal.Models.Entities;
using Clicks.Yoga.Portal.Models.Entities.Summaries;

namespace Clicks.Yoga.Portal.Models.SignUp
{
    [DataContract]
    public class ActivityCreateViewModel
    {
        public ActivityCreateViewModel()
        {
            AbilityLevel = new AbilityLevelSelectorModel();
            Date = new ActivityDateEditorModel(DateTime.Now.Date) { Minimum = DateTime.Now, MaximumYear = DateTime.Now.Year + 10 };
            EndDate = new ActivityDateEditorModel(DateTime.Now.Date) { Minimum = DateTime.Now, MaximumYear = DateTime.Now.Year + 10 };
            TimeRange = new TimeRangeEditorModel();
            RepeatFrequency = new ActivityRepeatFrequencySelectorModel();
            Styles = new SignUpStyleChooserModel();
            Conditions = new SignUpConditionChooserModel();
            Teachers = new ActivityTeacherChooserModel();
            Venue = new SignUpVenueSelectorCreatorModel();
        }

        public ActivityCreateViewModel(Profile profile, Teacher teacher, Venue venue, TeacherTrainingOrganisation tto)
        {
            AbilityLevel = new AbilityLevelSelectorModel();
            Date = new ActivityDateEditorModel(DateTime.Now.Date) { Minimum = DateTime.Now, MaximumYear = DateTime.Now.Year + 10 };
            EndDate = new ActivityDateEditorModel(DateTime.Now.Date) { Minimum = DateTime.Now, MaximumYear = DateTime.Now.Year + 10 };
            TimeRange = new TimeRangeEditorModel();
            RepeatFrequency = new ActivityRepeatFrequencySelectorModel();
            Styles = new SignUpStyleChooserModel();
            Conditions = new SignUpConditionChooserModel();
            Teachers = new ActivityTeacherChooserModel();
            Venue = new SignUpVenueSelectorCreatorModel();

            Profile = new ProfileModel(profile);

            if (teacher != null)
            {
                Teacher = new TeacherModel(teacher);
            }

            if (venue != null)
            {
                VenueModel = new VenueModel(venue);
            }

            if (tto != null)
            {
                TTO = new TeacherTrainingOrganisationModel(tto);
            }
        }

        public ProfileModel Profile { get; set; }

        public TeacherModel Teacher { get; set; }

        public VenueModel VenueModel { get; set; }

        public TeacherTrainingOrganisationModel TTO { get; set; }

        public bool HasTeacher
        {
            get { return Teacher != null; }
        }

        public bool HasVenue
        {
            get { return VenueModel != null; }
        }

        public bool HasTTO
        {
            get { return TTO != null; }
        }

        public ActivityCreateClassModel CreateClassModel { get; set; }

        public SignUpActivityTypeSelectorModel Type { get; private set; }

        public ActivitiesEntityHandleSelectorModel PromoterHandle { get; private set; }
        
        [DataMember]
        public int HiddenType { get; set; }

        public string ActivityTypeName { get; set; }

        [DataMember]
        public int HiddenHandle { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public AbilityLevelSelectorModel AbilityLevel { get; private set; }

        [DataMember]
        public ActivityDateEditorModel Date { get; private set; }

        [DataMember]
        public ActivityDateEditorModel EndDate { get; private set; }

        [DataMember]
        public SignUpVenueSelectorCreatorModel Venue { get; private set; }

        [DataMember]
        public TimeRangeEditorModel TimeRange { get; private set; }
        
        [DataMember]
        public bool BookingRequired { get; set; }

        [DataMember]
        public string Price { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public SignUpStyleChooserModel Styles { get; private set; }

        [DataMember]
        public SignUpConditionChooserModel Conditions { get; set; }
        
        [DataMember]
        public ActivityTeacherChooserModel Teachers { get; private set; }

        // Activity
        [DataMember]
        public bool Private { get; set; }
        //End Activity

        // Class
        [DataMember]
        public ActivityRepeatFrequencySelectorModel RepeatFrequency { get; private set; }

        [DataMember]
        public bool RepeatMonday { get; set; }
        [DataMember]
        public bool RepeatTuesday { get; set; }
        [DataMember]
        public bool RepeatWednesday { get; set; }
        [DataMember]
        public bool RepeatThursday { get; set; }
        [DataMember]
        public bool RepeatFriday { get; set; }
        [DataMember]
        public bool RepeatSaturday { get; set; }
        [DataMember]
        public bool RepeatSunday { get; set; }
        // End Class


        public void PopulateEntity(Activity activity, IActivityService activityService, IStyleService styleService, IMedicService medicService, ITeacherService teacherService, IVenueService venueService, IEntityService entityService, ISecurityContext securityContext)
        {
            var promoterHandle = entityService.GetEntityHandle(HiddenHandle);
            var profile = entityService.GetEntity<IProfileBanner>(promoterHandle);


            if (!securityContext.CanUpdate(promoterHandle))
                throw new PermissionDeniedException();


            activity.Name = Name;
            activity.Public = true;
            activity.AbilityLevel = activityService.GetAbilityLevel(AbilityLevel.Id);
            
            //Hidden type 1 means class (ActivtyType)
            if (HiddenType == 1)
            {
                activity.RepeatFrequency = RepeatFrequency.Id == 2 || RepeatFrequency.Id == 3 || RepeatFrequency.Id == 4 ? activityService.GetRepeatFrequency(RepeatFrequency.Id) : activityService.GetSingleRepeatFrequency();

                activity.StartTime = Date.DateTime.Value.Add(TimeRange.StartTimeSpan.Value);
                activity.FinishTime = Date.DateTime.Value.Add(TimeRange.FinishTimeSpan.Value); 
            }
            else
            {
                activity.Public = !Private;

                activity.StartTime = Date.DateTime.Value.Add(TimeRange.StartTimeSpan.Value);
                activity.FinishTime = EndDate.DateTime.Value.Add(TimeRange.FinishTimeSpan.Value); 
            }

            activity.BookingRequired = BookingRequired;
            activity.Price = Price ?? "";
            activity.Description = Description ?? "";
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

        public ActivityCreateViewModel PopulateMetadata(bool isClass ,IActivityService activityService, IStyleService styleService, IMedicService medicService, IEnumerable<Country> countries, ActivitiesEntityHandleSelectorModel promoterHandle, SignUpActivityTypeSelectorModel type)
        {
            AbilityLevel.PopulateMetadata(activityService.GetAbilityLevels());
            RepeatFrequency.PopulateMetadata(activityService.GetRepeatFrequencies());
            Styles.PopulateMetadata(styleService.GetStyles());
            Conditions.PopulateMetadata(medicService.GetConditions());
            Venue.PopulateMetadata(countries);
            PromoterHandle = promoterHandle;
            Type = type;

            if (HiddenType == 0)
            {
                HiddenType = Type.Id;
            }
            return this;
        }
        public ActivityCreateViewModel PopulateMetadataClone(Activity activity, IActivityService activityService, IStyleService styleService, IMedicService medicService,ITeacherService teacherService,IEnumerable<Country> countries)
        {
      
            Name = activity.Name;
            Description = activity.Description;
            BookingRequired = activity.BookingRequired;
            Price = activity.Price;

            AbilityLevel.Selection = activity.AbilityLevel.Id;



            TimeRange = new TimeRangeEditorModel(activity.StartTime, activity.FinishTime);
            Date = new ActivityDateEditorModel(activity.StartTime) { Minimum = DateTime.Now, MaximumYear = DateTime.Now.Year + 10 };
            EndDate = new ActivityDateEditorModel(activity.FinishTime) { Minimum = DateTime.Now, MaximumYear = DateTime.Now.Year + 10 };
            RepeatFrequency = new ActivityRepeatFrequencySelectorModel(activity.RepeatFrequency);

            






               Venue = new SignUpVenueSelectorCreatorModel();

               Venue.PopulateMetadata( countries);

            if (activity.ActivityVenue != null)
            {
                Venue.Name = activity.ActivityVenue.Venue.Name;
                Venue.Id = activity.ActivityVenue.Venue.Id;

            }
               


    
            RepeatFrequency.PopulateMetadata(activityService.GetRepeatFrequencies());


            ICollection<Teacher> x = new List<Teacher>();

            foreach (var teacher in activity.Teachers)
            {
                // 7883
                x.Add(teacherService.GetTeacher(teacher.Teacher.Id));



            }
            //Teachers.PopulateMetadata(x);

            if (x.Count > 0)
            {
                Teachers = new ActivityTeacherChooserModel(x);  
            }
          



            Conditions = new SignUpConditionChooserModel(activity.Conditions);

            Conditions.PopulateMetadata(medicService.GetConditions());



            Styles = new SignUpStyleChooserModel(activity.Styles);

            Styles.PopulateMetadata(styleService.GetStyles());



        
            foreach (var condition in activity.Conditions)
            {
                Conditions.SelectedEntities.Add(new NamedSummaryModel<Condition>(condition));
            }


            foreach (var styles in activity.Styles)
            {
                Styles.SelectedEntities.Add(new NamedSummaryModel<Style>(styles));
            }


            return this;
        }

    }
}