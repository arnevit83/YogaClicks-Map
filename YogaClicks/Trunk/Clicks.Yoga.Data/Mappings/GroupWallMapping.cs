using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class GroupWallMapping : EntityMapping<GroupWall>
    {
        public GroupWallMapping()
        {
            ToTable("GroupWall");
            HasRequired(e => e.Group).WithRequiredPrincipal(e => e.Wall).Map(a => a.MapKey("WallId"));
        }
    }
}