using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class TeacherPlacementMapping : EntityMapping<TeacherPlacement>
    {
        public TeacherPlacementMapping()
        {
            HasRequired(e => e.Teacher).WithMany(e => e.Placements).Map(a => a.MapKey("TeacherId"));
            HasRequired(e => e.Venue).WithMany(e => e.TeacherPlacements).Map(a => a.MapKey("VenueId"));
        }
    }
}