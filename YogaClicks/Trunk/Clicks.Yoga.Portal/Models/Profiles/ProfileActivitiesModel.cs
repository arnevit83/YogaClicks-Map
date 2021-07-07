using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Profiles
{
    public class ProfileActivitiesModel
    {
        public ProfileActivitiesModel(Profile profile)
        {
            Profile = new ProfileModel(profile);
        }

        public ProfileModel Profile { get; private set; }
    }
}