using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Profiles
{
    public class ProfileReviewsModel
    {
        public ProfileReviewsModel(Profile profile)
        {
            Profile = new ProfileModel(profile);
        }

        public ProfileModel Profile { get; set; }
    }
}