using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.TeacherTrainingOrganisations
{
    public class OrganisationStylesUpdateModel
    {
        public OrganisationStylesUpdateModel()
        {
            Styles = new StyleChooserModel();
        }

        public OrganisationStylesUpdateModel(TeacherTrainingOrganisation entity, IStyleService styleService)
        {
            EntityId = entity.Id;
            Styles = new StyleChooserModel(entity.Styles, styleService.GetStyles());
        }

        public int EntityId { get; set; }

        public StyleChooserModel Styles { get; set; }

        public void PopulateEntity(
            TeacherTrainingOrganisation entity,
             IStyleService styleService)
        {
            Styles.PopulateCollection(entity.Styles, styleService);
        }
    }
}