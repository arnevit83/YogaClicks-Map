using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Venues
{
    public class VenueStylesUpdateModel
    {
          public VenueStylesUpdateModel()
        {
            Styles = new StyleChooserModel();
        }

          public VenueStylesUpdateModel(Venue venue)
        {
            VenueId = venue.Id;
            Styles = new StyleChooserModel(venue.Styles);
        }

        public void PopulateEntity(
            Venue entity,
             IStyleService styleService)
        {
            Styles.PopulateCollection(entity.Styles, styleService);
        }

        public int VenueId { get; set; }

        public StyleChooserModel Styles { get; private set; }

        public VenueStylesUpdateModel PopulateMetadata(
            Venue venue,
            IStyleService styleService)
        {
            Styles.PopulateMetadata(styleService.GetStyles());
            return this;
        }
    }
}