using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Profiles
{
    public class ProfileFriendsModel
    {
        public ProfileFriendsModel(Profile profile)
        {
            Profile = new ProfileModel(profile);
        }

        public ProfileModel Profile { get; private set; }
    }
}