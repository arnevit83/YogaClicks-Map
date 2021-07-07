using System;

namespace Clicks.Yoga.Portal.Models.Editors
{
    public class MonthEditorModel
    {
        public MonthEditorModel() {}

        public MonthEditorModel(DateTime? date)
        {
            if (date == null) return;

            Year = date.Value.Year;
            Month = date.Value.Month;
        }

        public int? Year { get; set; }

        public int? Month { get; set; }

        public bool HasValue
        {
            get { return Year.HasValue || Month.HasValue; }

        }

        public bool IsValid
        {
            get
            {
                if (!HasValue) return true;

                try
                {
                    ToDateTime();
                }
                catch (InvalidOperationException)
                {
                    return false;
                }

                return true;
            }
        }

        public DateTime? ToDateTime()
        {
            if (!HasValue) return null;
            return new DateTime(Year.Value, Month.Value, 1);
        }
    }
}