using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Profiles
{
    public class ProfileImageUpdateModel
    {
        public ProfileImageUpdateModel()
        {
        }

        public ProfileImageUpdateModel(Profile profile)
        {
            Image = new ProfileImageEditorModel(profile.Image);
            ProfileId = profile.Id;
        }
        public ProfileImageEditorModel Image { get; set; }

        public int ProfileId { get; set; }

        public void PopulateEntity(
            Profile profile,
            IImageService imageService)
        {
            profile.Image = Image.PopulateEntity(() => profile.Image, imageService);
        }
    }
}