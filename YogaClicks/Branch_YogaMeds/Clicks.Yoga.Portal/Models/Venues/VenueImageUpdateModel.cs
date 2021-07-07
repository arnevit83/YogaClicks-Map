using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Venues
{
    public class VenueImageUpdateModel
    {
        public VenueImageUpdateModel()
        {
        }

        public VenueImageUpdateModel(Venue venue)
        {
            Image = new ProfileImageEditorModel(venue.Image);
            VenueId = venue.Id;
        }

        public void PopulateEntity(
            Venue entity,
            IImageService imageService)
        {
            entity.Image = Image.PopulateEntity(() => entity.Image, imageService);
        }

        public int VenueId { get; set; }
        public ProfileImageEditorModel Image { get; set; }
    }
}