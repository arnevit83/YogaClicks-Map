using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Teachers
{
    public class TeacherDetailsUpdateModel
    {
        public TeacherDetailsUpdateModel()
        {
        }

        public TeacherDetailsUpdateModel(Teacher teacher)
        {
            Name = teacher.Name;
            Websites = new WebsiteCollectionEditorModel(teacher.Websites);
            TeacherId = teacher.Id;
            Telephone = teacher.Telephone;
            EmailAddress = teacher.EmailAddress;
        }

        public void PopulateEntity(
            Teacher entity,
            IWebsiteService websiteService)
        {
            entity.Name = Name;
            entity.Telephone = Telephone;
            entity.EmailAddress = EmailAddress;
            Websites.PopulateCollection(entity.Websites, websiteService);
        }

        public int TeacherId { get; set; }

        public string Name { get; set; }

        public string Telephone { get; set; }

        public string EmailAddress { get; set; }

        public WebsiteCollectionEditorModel Websites { get; set; }
    }
}