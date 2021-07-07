using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Profiles
{
    public class ProfileBannerUpdateModel
    {
        public ProfileBannerUpdateModel()
        {
        }

        public ProfileBannerUpdateModel(Profile profile)
        {
            ProfileBanner = new ProfileBannerEditorModel(profile.ProfileBanner);
            ProfileId = profile.Id;
        }
        public ProfileBannerEditorModel ProfileBanner { get; set; }

        public int ProfileId { get; set; }

        public void PopulateEntity(
            Profile profile,
            IImageService imageService)
        {
            profile.ProfileBanner = ProfileBanner.PopulateEntity(() => profile.ProfileBanner, imageService);
        }
    }
}