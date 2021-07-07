using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Profiles
{
    public class ProfileGalleriesModel
    {
        public ProfileGalleriesModel(Profile profile)
        {
            Profile = new ProfileModel(profile);
        }

        public ProfileModel Profile { get; private set; }
    }
}