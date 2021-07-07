using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services.Interfaces;
using Clicks.Yoga.Portal.Models.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.TeacherTrainingOrganisations
{
    public class OrganisationConditionsUpdateModel
    {
        public OrganisationConditionsUpdateModel()
        {
            Conditions = new ConditionChooserModel();
        }

        public OrganisationConditionsUpdateModel(TeacherTrainingOrganisation entity, IMedicService medicService)
        {
            EntityId = entity.Id;
            Conditions = new ConditionChooserModel(entity.Conditions, medicService.GetConditions());
        }

        public int EntityId { get; set; }

        public ConditionChooserModel Conditions { get; set; }

        public void PopulateEntity(
            TeacherTrainingOrganisation entity,
             IMedicService medicService)
        {
            Conditions.PopulateCollection(entity.Conditions, medicService);
        }
    }
}