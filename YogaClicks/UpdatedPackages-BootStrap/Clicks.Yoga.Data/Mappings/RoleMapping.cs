using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class RoleMapping : EntityMapping<Role>
    {
        public RoleMapping()
        {
            Property(e => e.Name).IsRequired().HasMaxLength(100);
        }
    }
}