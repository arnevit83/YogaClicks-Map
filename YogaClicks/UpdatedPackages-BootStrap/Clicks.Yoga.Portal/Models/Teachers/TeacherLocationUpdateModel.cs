using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Teachers
{
    public class TeacherLocationUpdateModel
    {
        public TeacherLocationUpdateModel()
        {
        }

        public TeacherLocationUpdateModel(Teacher teacher)
        {
            TeacherId = teacher.Id;
            Location = new LocationEditorModel(teacher.Location);
        }

        public void PopulateEntity(
            Teacher entity)
        {
            entity.Location = Location.PopulateEntity(entity.Location);
        }

        public int TeacherId { get; set; }

        public LocationEditorModel Location { get; set; }
    }
}