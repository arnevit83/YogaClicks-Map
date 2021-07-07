using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class UserRoleMapping : EntityMapping<UserRole>
    {
        public UserRoleMapping()
        {
            HasRequired(e => e.User).WithMany().Map(a => a.MapKey("UserId")).WillCascadeOnDelete(false);
            HasRequired(e => e.Role).WithMany().Map(a => a.MapKey("RoleId")).WillCascadeOnDelete(false);
        }
    }
}