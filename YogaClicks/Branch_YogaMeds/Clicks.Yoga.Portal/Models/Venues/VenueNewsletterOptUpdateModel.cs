using Clicks.Yoga.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Venues
{
    public class VenueNewsletterOptUpdateModel
    {
        public VenueNewsletterOptUpdateModel()
        {
        }

        public VenueNewsletterOptUpdateModel(Venue venue)
        {
            VenueId = venue.Id;
            NewsletterOptOut = venue.NewsletterOptOut;
        }

        public int VenueId { get; set; }

        public bool NewsletterOptOut { get; set; }

        public void PopulateEntity(
            Venue entity)
        {
            entity.NewsletterOptOut = NewsletterOptOut;
        }
    }
}