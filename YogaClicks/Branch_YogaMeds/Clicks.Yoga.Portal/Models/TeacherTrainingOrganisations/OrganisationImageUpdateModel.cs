using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.TeacherTrainingOrganisations
{
    public class OrganisationImageUpdateModel
    {
        public OrganisationImageUpdateModel()
        {
        }

        public OrganisationImageUpdateModel(TeacherTrainingOrganisation entity)
        {
            EntityId = entity.Id;
            Image = new ProfileImageEditorModel(entity.Image);
        }

        public int EntityId { get; set; }

        public ProfileImageEditorModel Image { get; set; }

        public void PopulateEntity(
            TeacherTrainingOrganisation entity,
             IImageService imageService)
        {
            entity.Image = Image.PopulateEntity(() => entity.Image, imageService);
        }
    }
}