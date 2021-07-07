using System;
using System.Runtime.Serialization;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Editors;
using Clicks.Yoga.Domain.Services;

namespace Clicks.Yoga.Portal.Models.Activities
{
    [DataContract]
    public class ActivityCreateStep2Model
    {
        public ActivityCreateStep2Model()
        {
            StartTime = new DateTimeEditorModel(DateTime.Now.Date.AddDays(1)) { Minimum = DateTime.Now.Date, MaximumYear = DateTime.Now.Year + 10 };
            FinishTime = new DateTimeEditorModel(DateTime.Now.Date.AddDays(1)) { Minimum = DateTime.Now.Date, MaximumYear = DateTime.Now.Year + 10 };
            Styles = new StyleChooserModel();
        }

        [DataMember]
        public DateTimeEditorModel StartTime { get; private set; }

        [DataMember]
        public DateTimeEditorModel FinishTime { get; private set; }

        [DataMember]
        public bool BookingRequired { get; set; }

        [DataMember]
        public string Price { get; set; }
        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public StyleChooserModel Styles { get; private set; }

        public bool Back { get; set; }

        public void PopulateEntity(Activity entity, IStyleService styleService)
        {
            entity.StartTime = StartTime.DateTime.Value;
            entity.FinishTime = FinishTime.DateTime.Value;
            entity.BookingRequired = BookingRequired;
            entity.Price = Price ?? "";
            entity.Description = Description;
            Styles.PopulateCollection(entity.Styles, styleService);
        }

        public ActivityCreateStep2Model PopulateMetadata(IStyleService styleService)
        {
            Styles.PopulateMetadata(styleService.GetStyles());
            return this;
        }
    }
}