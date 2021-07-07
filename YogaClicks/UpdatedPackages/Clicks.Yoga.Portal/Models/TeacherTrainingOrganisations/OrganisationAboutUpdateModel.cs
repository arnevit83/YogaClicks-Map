using Clicks.Yoga.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.TeacherTrainingOrganisations
{
    public class OrganisationAboutUpdateModel
    {
          public OrganisationAboutUpdateModel()
        {
        }

          public OrganisationAboutUpdateModel(TeacherTrainingOrganisation entity)
        {
            EntityId = entity.Id;
            Description = entity.Description;
        }

        public int EntityId { get; set; }

        public string Description { get; set; }

        public void PopulateEntity(
            TeacherTrainingOrganisation entity)
        {
            entity.Description = Description;
        }
    }
}