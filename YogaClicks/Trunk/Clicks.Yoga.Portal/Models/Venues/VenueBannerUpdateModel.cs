using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Venues
{
    public class VenueBannerUpdateModel
    {
         public VenueBannerUpdateModel()
        {
        }

         public VenueBannerUpdateModel(Venue venue)
        {
            ProfileBanner = new ProfileBannerEditorModel(venue.ProfileBanner);
            VenueId = venue.Id;
        }
        public ProfileBannerEditorModel ProfileBanner { get; set; }

        public int VenueId { get; set; }

        public void PopulateEntity(
            Venue entity,
            IImageService imageService)
        {
            entity.ProfileBanner = ProfileBanner.PopulateEntity(() => entity.ProfileBanner, imageService);
        }
    }
}