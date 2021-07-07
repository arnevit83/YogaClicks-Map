namespace Clicks.Yoga.Domain.Entities
{
    public class Friendship : Entity, INewsLink
    {
        public virtual Profile Profile { get; set; }

        public virtual Profile Friend { get; set; }

        public bool Pending { get; set; }

        public bool Confirmed { get; set; }

        public bool BlockedByProfile { get; set; }

        public bool BlockedByFriend { get; set; }

        public virtual Friendship Inverse { get; set; }

        public FriendshipStatus Status
        {
            get
            {
                if (BlockedByProfile)
                {
                    return FriendshipStatus.BlockedBySelf;
                }
                else if (BlockedByFriend)
                {
                    return FriendshipStatus.BlockedByFriend;
                }
                else if (Pending)
                {
                    return FriendshipStatus.Pending;
                }
                else if (Confirmed)
                {
                    return FriendshipStatus.Friends;
                }
                else
                {
                    return FriendshipStatus.Strangers;
                }
            }
        }

        bool INewsLink.NewsRequired
        {
            get { return Confirmed; }
        }

        Profile INewsLink.NewsSubscriber
        {
            get { return Profile; }
        }

        IEntityHandle INewsLink.NewsSubject
        {
            get { return Friend; }
        }
    }
}