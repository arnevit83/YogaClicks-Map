using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Profiles
{
    public class ProfileLocationUpdateModel
    {
        public ProfileLocationUpdateModel()
        {
        }

        public ProfileLocationUpdateModel(Profile profile)
        {
            ProfileId = profile.Id;
            Location = new LocationEditorModel(profile.Location);
            Location.ShowCityOnly = true;
        }

        public int ProfileId { get; set; }

        public LocationEditorModel Location { get; set; }

        public void PopulateEntity(
            Profile profile)
        {
            profile.Location = Location.PopulateEntity(profile.Location);
        }
    }
}