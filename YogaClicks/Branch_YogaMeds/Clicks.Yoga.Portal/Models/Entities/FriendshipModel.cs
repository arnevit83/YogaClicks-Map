using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class FriendshipModel : EntityModel<Friendship>
    {
        public FriendshipModel(Friendship entity) : base(entity) {}

        public ProfileModel Friend { get; private set; }

        public bool Confirmed { get; private set; }

        public bool BlockedByProfile { get; private set; }

        public bool BlockedByFriend { get; private set; }

        public override void Populate(Friendship entity)
        {
            Friend = new ProfileModel(entity.Friend);
            Confirmed = entity.Confirmed;
            BlockedByProfile = entity.BlockedByProfile;
            BlockedByFriend = entity.BlockedByFriend;
        }
    }
}