using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class FriendshipMapping : EntityMapping<Friendship>
    {
        public FriendshipMapping()
        {
            HasRequired(e => e.Profile).WithMany(e => e.Friendships).Map(a => a.MapKey("ProfileId"));
            HasRequired(e => e.Friend).WithMany().Map(a => a.MapKey("FriendId")).WillCascadeOnDelete(false);
            Property(e => e.Pending).IsRequired();
            Property(e => e.Confirmed).IsRequired();
            Property(e => e.BlockedByProfile).IsRequired();
            Property(e => e.BlockedByFriend).IsRequired();
            HasOptional(e => e.Inverse).WithOptionalDependent().Map(a => a.MapKey("InverseId"));
        }
    }
}