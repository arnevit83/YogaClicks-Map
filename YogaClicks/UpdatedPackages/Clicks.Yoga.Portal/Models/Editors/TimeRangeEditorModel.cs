using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web.Mvc;

namespace Clicks.Yoga.Portal.Models.Editors
{
    [DataContract]
    public class TimeRangeEditorModel
    {
        public TimeRangeEditorModel() { }

        public TimeRangeEditorModel(DateTime? startTime, DateTime? finishTime)
        {
            if (startTime.HasValue)
            {
                StartHour = startTime.Value.Hour;
                StartMinute = startTime.Value.Minute;
            }

            if (finishTime.HasValue)
            {
                FinishHour = finishTime.Value.Hour;
                FinishMinute = finishTime.Value.Minute;
            }
        }

        public bool Optional { get; set; }

        [DataMember]
        public int? StartHour { get; set; }

        [DataMember]
        public int? StartMinute { get; set; }

        [DataMember]
        public int? FinishHour { get; set; }

        [DataMember]
        public int? FinishMinute { get; set; }

        public bool HasValue
        {
            get { return StartHour.HasValue && StartMinute.HasValue && FinishHour.HasValue && FinishMinute.HasValue; }
        }

        public bool IsValid
        {
            get
            {
                if (!HasValue) return Optional;

                try
                {
                    StartToDateTime();
                    FinishToDateTime();
                }
                catch (ArgumentOutOfRangeException)
                {
                    return false;
                }

                //if (FinishToDateTime() <= StartToDateTime()) return false;

                return true;
            }
        }

        public DateTime? StartTime
        {
            get
            {
                if (!HasValue) return null;
                if (!IsValid) throw new InvalidOperationException("Current state represents an invalid time range.");

                return StartToDateTime();
            }
        }

        public DateTime? FinishTime
        {
            get
            {
                if (!HasValue) return null;
                if (!IsValid) throw new InvalidOperationException("Current state represents an invalid time range.");

                return FinishToDateTime();
            }
        }

        public TimeSpan? StartTimeSpan
        {
            get
            {
                if (!HasValue) return null;
                if (!IsValid) throw new InvalidOperationException("Current state represents an invalid time range.");

                return new TimeSpan(0, StartHour.Value, StartMinute.Value, 0);
            }
        }

        public TimeSpan? FinishTimeSpan
        {
            get
            {
                if (!HasValue) return null;
                if (!IsValid) throw new InvalidOperationException("Current state represents an invalid time range.");

                return new TimeSpan(0, FinishHour.Value, FinishMinute.Value, 0);
            }
        }

        public IList<SelectListItem> HourOptions
        {
            get { return Enumerable.Range(0, 24).Select(v => new SelectListItem { Value = v.ToString("0"), Text = v.ToString("00") }).ToList(); }
        }

        public IList<SelectListItem> MinuteOptions
        {
            get { return Enumerable.Range(0, 60).Select(v => new SelectListItem { Value = v.ToString("0"), Text = v.ToString("00") }).ToList(); }
        }

        protected DateTime StartToDateTime()
        {
            return new DateTime(1, 1, 1, StartHour.Value, StartMinute.Value, 0);
        }

        protected DateTime FinishToDateTime()
        {
            return new DateTime(1, 1, 1, FinishHour.Value, FinishMinute.Value, 0);
        }
    }
}