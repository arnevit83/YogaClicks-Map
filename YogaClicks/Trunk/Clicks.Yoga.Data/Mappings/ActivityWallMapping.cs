using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class ActivityWallMapping : EntityMapping<ActivityWall>
    {
        public ActivityWallMapping()
        {
            ToTable("ActivityWall");
            HasRequired(e => e.Activity).WithRequiredPrincipal(e => e.Wall).Map(a => a.MapKey("WallId"));
        }
    }
}