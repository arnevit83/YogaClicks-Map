using Clicks.Yoga.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Teachers
{
    public class TeacherNewsletterOptUpdateModel
    {
        public TeacherNewsletterOptUpdateModel()
        {
        }

        public TeacherNewsletterOptUpdateModel(Teacher teacher)
        {
            TeacherId = teacher.Id;
            NewsletterOptOut = teacher.NewsletterOptOut;
        }

        public void PopulateEntity(
            Teacher entity)
        {
            entity.NewsletterOptOut = NewsletterOptOut;
        }

        public int TeacherId { get; set; }

        public bool NewsletterOptOut { get; set; }
    }
}