using System;
using System.Runtime.Serialization;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.Activities
{
    [DataContract]
    public class ActivityCreateClassStep2Model
    {
        public ActivityCreateClassStep2Model()
        {
            AbilityLevel = new AbilityLevelSelectorModel();
            Date = new DateEditorModel(DateTime.Now.Date) { Minimum = DateTime.Now, MaximumYear = DateTime.Now.Year + 10 };
            TimeRange = new TimeRangeEditorModel();
            RepeatFrequency = new ActivityRepeatFrequencySelectorModel();
        }

        public ActivityCreateClassModel CreateClassModel { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public AbilityLevelSelectorModel AbilityLevel { get; private set; }

        [DataMember]
        public DateEditorModel Date { get; private set; }

        [DataMember]
        public TimeRangeEditorModel TimeRange { get; private set; }

        [DataMember]
        public bool Repeat { get; set; }

        [DataMember]
        public ActivityRepeatFrequencySelectorModel RepeatFrequency { get; private set; }

        public bool Back { get; set; }

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

        public void PopulateEntity(Activity activity, IActivityService activityService)
        {
            activity.Name = Name;
            activity.Public = true;
            activity.AbilityLevel = activityService.GetAbilityLevel(AbilityLevel.Id);
            activity.RepeatFrequency = Repeat ? activityService.GetRepeatFrequency(RepeatFrequency.Id) : activityService.GetSingleRepeatFrequency();
            activity.StartTime = Date.DateTime.Value.Add(TimeRange.StartTimeSpan.Value);
            activity.FinishTime = Date.DateTime.Value.Add(TimeRange.FinishTimeSpan.Value);
        }

        public ActivityCreateClassStep2Model PopulateMetadata(IActivityService activityService)
        {
            AbilityLevel.PopulateMetadata(activityService.GetAbilityLevels());
            RepeatFrequency.PopulateMetadata(activityService.GetRepeatFrequencies());
            return this;
        }
    }
}