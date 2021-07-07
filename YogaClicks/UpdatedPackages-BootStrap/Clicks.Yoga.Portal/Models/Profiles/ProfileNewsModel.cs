using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Profiles
{
    public class ProfileNewsModel
    {
        public ProfileNewsModel(Profile profile)
        {
            Profile = new ProfileModel(profile);
        }

        public ProfileModel Profile { get; set; }
    }
}