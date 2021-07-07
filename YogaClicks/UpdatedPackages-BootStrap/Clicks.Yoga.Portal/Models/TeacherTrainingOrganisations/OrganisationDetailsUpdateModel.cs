using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.TeacherTrainingOrganisations
{
    public class OrganisationDetailsUpdateModel
    {
         public OrganisationDetailsUpdateModel()
        {
            Website = new WebsiteEditorModel();
        }

         public OrganisationDetailsUpdateModel(TeacherTrainingOrganisation entity)
        {
            EntityId = entity.Id;
            Name = entity.Name;
            Founder = entity.Founder;
            Website = new WebsiteEditorModel(entity.Website);
            EmailAddress = entity.EmailAddress;
        }

        public int EntityId { get; set; }

        public string Name { get; set; }

        public string Founder { get; set; }

        public string EmailAddress { get; set; }

        public WebsiteEditorModel Website { get; set; }

        public void PopulateEntity(
            TeacherTrainingOrganisation entity,
             IWebsiteService websiteService)
        {
            entity.Website = Website.PopulateEntity(entity.Website, websiteService);
            entity.Name = Name;
            entity.Founder = Founder;
            entity.EmailAddress = EmailAddress;
        }
    }
}