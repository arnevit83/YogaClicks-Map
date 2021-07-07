using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Profiles
{
    public class ProfileUpdateStudentLocationModel
    {
        public ProfileUpdateStudentLocationModel()
        {
            Gender = new GenderSelectorModel();
            BirthDate = new DateEditorModel(new DateTime(1980, 1, 1)) { Optional = true };
            BirthDate.MinmumYear = DateTime.Now.Year - 123;
            BirthDate.Maximum = DateTime.Now.Date.AddYears(-18);
        }

        public LocationEditorModel Location { get; set; }

        public GenderSelectorModel Gender { get; set; }

        public DateEditorModel BirthDate { get; set; }
     }
}