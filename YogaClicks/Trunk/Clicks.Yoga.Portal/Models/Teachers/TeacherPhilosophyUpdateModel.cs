using Clicks.Yoga.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Teachers
{
    public class TeacherPhilosophyUpdateModel
    {
        public TeacherPhilosophyUpdateModel()
        {
        }

        public TeacherPhilosophyUpdateModel(Teacher teacher)
        {
            TeacherId = teacher.Id;
            Philosophy = teacher.Philosophy;
        }

        public void PopulateEntity(
            Teacher entity)
        {
            entity.Philosophy = Philosophy;
        }

        public int TeacherId { get; set; }

        public string Philosophy { get; set; }
    }
}