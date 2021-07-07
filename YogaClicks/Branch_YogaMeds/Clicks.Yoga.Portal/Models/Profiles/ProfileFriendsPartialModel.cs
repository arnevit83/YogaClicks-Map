using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Profiles
{
    public class ProfileFriendsPartialModel
    {
        public ProfileFriendsPartialModel(Profile profile, IEnumerable<Friendship> friendships)
        {
            Profile = new ProfileModel(profile);
            Friendships = new List<FriendshipModel>();
            BlockedFriendships = new List<FriendshipModel>();

            foreach (var friendship in friendships)
            {
                if (!friendship.Confirmed || friendship.BlockedByFriend) continue;

                var friendshipModel = new FriendshipModel(friendship);

                if (friendship.BlockedByProfile)
                {
                    BlockedFriendships.Add(friendshipModel);
                }
                else
                {
                    Friendships.Add(friendshipModel);
                }
            }
        }

        public ProfileModel Profile { get; private set; }

        public IList<FriendshipModel> Friendships { get; private set; }

        public IList<FriendshipModel> BlockedFriendships { get; private set; } 
    }
}