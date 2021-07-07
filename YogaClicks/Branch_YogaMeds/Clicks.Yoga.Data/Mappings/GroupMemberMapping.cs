using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class GroupMemberMapping : EntityMapping<GroupMember>
    {
        public GroupMemberMapping()
        {
            HasRequired(e => e.Group).WithMany(e => e.Members).Map(a => a.MapKey("GroupId"));
            HasRequired(e => e.User).WithMany(e => e.Memberships).Map(a => a.MapKey("UserId"));
            Property(e => e.Administrator).IsRequired();
            Property(e => e.Confirmed).IsRequired();
        }
    }
}