using System;
using System.Runtime.Serialization;

namespace Clicks.Yoga.Portal.Models.Editors
{
    [DataContract]
    public class DateTimeEditorModel : DateEditorModel
    {
        public DateTimeEditorModel() {}

        public DateTimeEditorModel(DateTime? date) : base(date)
        {
            if (date == null) return;

            Hour = date.Value.Hour;
            Minute = date.Value.Minute;
        }

        [DataMember]
        public int? Hour { get; set; }

        [DataMember]
        public int? Minute { get; set; }

        public override bool HasValue
        {
            get { return base.HasValue && Hour.HasValue && Minute.HasValue; }

        }

        protected override DateTime? ToDateTime()
        {
            return new DateTime(Year.Value, Month.Value, Day.Value, Hour.Value, Minute.Value, 0);
        }
    }
}