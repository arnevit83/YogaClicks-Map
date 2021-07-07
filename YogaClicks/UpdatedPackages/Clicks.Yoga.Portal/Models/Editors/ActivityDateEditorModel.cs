using System;
using System.Runtime.Serialization;

namespace Clicks.Yoga.Portal.Models.Editors
{
    [DataContract]
    public class ActivityDateEditorModel
    {
        public ActivityDateEditorModel() {}

        public ActivityDateEditorModel(DateTime? date)
        {
            if (date == null) return;

            Year = date.Value.Year;
            Month = date.Value.Month;
            Day = date.Value.Day;
        }

        [DataMember]
        public DateTime Minimum { get; set; }

        [DataMember]
        public DateTime Maximum { get; set; }

        public int MinmumYear
        {
            get { return Minimum.Year; }
            set { Minimum = new DateTime(value, 1, 1); }
        }

        public int MaximumYear
        {
            get { return Maximum.Year; }
            set { Maximum = new DateTime(value, 12, 31); }
        }

        public bool Optional { get; set; }

        [DataMember]
        public int? Year { get; set; }

        [DataMember]
        public int? Month { get; set; }

        [DataMember]
        public int? Day { get; set; }

        public virtual bool HasValue
        {
            get { return Year.HasValue && Month.HasValue && Day.HasValue; }

        }

        public bool IsValid
        {
            get
            {
                if (!HasValue) return Optional;

                try
                {
                    ToDateTime();
                }
                catch (ArgumentOutOfRangeException)
                {
                    return false;
                }

                return true;
            }
        }

        public bool IsOverEighteenYearsAgo
        {
            get
            {
                if (!HasValue || !IsValid) return false;

                return ToDateTime().Value <= System.DateTime.Today.AddYears(-18);
            }
        }

        public virtual DateTime? DateTime
        {
            get
            {
                if (!HasValue) return null;
                if (!IsValid) throw new InvalidOperationException("Current state represents an invalid date.");

                return ToDateTime();
            }
        }

        protected virtual DateTime? ToDateTime()
        {
            return new DateTime(Year.Value, Month.Value, Day.Value);
        }
    }
}