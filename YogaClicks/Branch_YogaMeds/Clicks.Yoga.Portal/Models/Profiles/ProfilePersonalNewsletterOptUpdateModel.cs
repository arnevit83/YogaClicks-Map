using Clicks.Yoga.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Profiles
{
    public class ProfilePersonalNewsletterOptUpdateModel
    {
         public ProfilePersonalNewsletterOptUpdateModel()
        {
        }

         public ProfilePersonalNewsletterOptUpdateModel(Profile profile)
        {
            ProfileId = profile.Id;
            NewsletterOptOut = profile.NewsletterOptOut;
        }
         public bool NewsletterOptOut { get; set; }

        public int ProfileId { get; set; }

        public void PopulateEntity(
            Profile profile)
        {
            profile.NewsletterOptOut = NewsletterOptOut;
        }
    }
}