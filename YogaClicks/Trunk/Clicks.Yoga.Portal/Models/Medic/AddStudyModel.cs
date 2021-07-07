using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Medic
{
    public class AddStudyModel
    {
        public int ConditionId { get; set; }

        public string Title { get; set; }

        public string Abstract { get; set; }

        public string DateOfStudy { get; set; }

        public string Journal { get; set; }

        public string Volume { get; set; }

        public string Institution { get; set; }

        public string Source { get; set; }

        public int NumberOfCitations { get; set; }

        public List<TherapyTypeModel> TherapyTypes { get; set; }

        public List<AuthorModel> Authors { get; set; }
    }
}