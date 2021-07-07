using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.TeacherTrainingOrganisations
{
    public class OrganisationBannerUpdateModel
    {
        public OrganisationBannerUpdateModel()
        {
        }

        public OrganisationBannerUpdateModel(TeacherTrainingOrganisation entity)
        {
            EntityId = entity.Id;
            ProfileBanner = new ProfileBannerEditorModel(entity.ProfileBanner);
        }

        public int EntityId { get; set; }

        public ProfileBannerEditorModel ProfileBanner { get; set; }

        public void PopulateEntity(
            TeacherTrainingOrganisation entity,
             IImageService imageService)
        {
            entity.ProfileBanner = ProfileBanner.PopulateEntity(() => entity.ProfileBanner, imageService);
        }
    }
}