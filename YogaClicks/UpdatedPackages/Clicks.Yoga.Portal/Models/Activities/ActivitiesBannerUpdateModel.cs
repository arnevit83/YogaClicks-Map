using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Activities
{
    public class ActivitiesBannerUpdateModel
    {
        public ActivitiesBannerUpdateModel()
        {
            ProfileBanner = new ProfileBannerEditorModel();
        }

        public ActivitiesBannerUpdateModel(Activity activity)
        {
            ProfileBanner = new ProfileBannerEditorModel(activity.ProfileBanner);
            ActivityId = activity.Id;
        }
        public ProfileBannerEditorModel ProfileBanner { get; set; }

        public int ActivityId { get; set; }

        public void PopulateEntity(
            Activity entity,
            IImageService imageService)
        {
            entity.ProfileBanner = ProfileBanner.PopulateEntity(() => entity.ProfileBanner, imageService);
        }

    }
}