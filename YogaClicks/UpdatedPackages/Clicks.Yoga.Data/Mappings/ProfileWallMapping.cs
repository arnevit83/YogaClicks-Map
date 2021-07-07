using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class ProfileWallMapping : EntityMapping<ProfileWall>
    {
        public ProfileWallMapping()
        {
            ToTable("ProfileWall");
            HasRequired(e => e.Profile).WithRequiredPrincipal(e => e.Wall).Map(a => a.MapKey("WallId"));
        }
    }
}