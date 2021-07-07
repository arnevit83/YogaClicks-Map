using System;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.TeacherTrainingCourses
{
    [Serializable]
    public class CourseCreateNotesModel
    {
        public string PreRequisites { get; set; }

        public bool Back { get; set; }

        public string Notes { get; set; }

        public void PopulateEntity(TeacherTrainingCourse entity)
        {
            entity.PreRequisites = PreRequisites;
            entity.Notes = Notes;
        }
    }
}