using Clicks.Yoga.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Venues
{
    public class VenueAboutUpdateModel
    {
           public VenueAboutUpdateModel()
        {
        }

           public VenueAboutUpdateModel(Venue venue)
        {
            VenueId = venue.Id;
            Description = venue.Description;
        }

        public void PopulateEntity(
            Venue entity)
        {
            entity.Description = Description;
        }

        public int VenueId { get; set; }

        public string Description { get; set; }
    }
}