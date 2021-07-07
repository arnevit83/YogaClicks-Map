using Clicks.Yoga.Domain;

namespace Clicks.Yoga.Portal.Models.Friends
{
    public class FriendButtonModel
    {
        public FriendButtonModel(int profileId, string profileName, FriendshipStatus status)
        {
            ProfileId = profileId;
            ProfileName = profileName;
            FriendshipStatus = status;
        }

        public int ProfileId { get; private set; }

        public string ProfileName { get; private set; }

        public FriendshipStatus FriendshipStatus { get; private set; }
    }
}