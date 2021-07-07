using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Teachers;
using Clicks.Yoga.Portal.Models.Venues;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class TeacherPlacementModel : EntityModel<TeacherPlacement>
    {
        public TeacherPlacementModel(TeacherPlacement entity) : base(entity) {}

        public TeacherPlacementTeacherModel Teacher { get; set; }

        public TeacherPlacementVenueModel Venue { get; set; }

        public bool Confirmed { get; set; }

        public override void Populate(TeacherPlacement entity)
        {
            Id = entity.Id;
            Teacher = new TeacherPlacementTeacherModel(entity.Teacher);
            Venue = new TeacherPlacementVenueModel(entity.Venue);
            Confirmed = entity.Confirmed;
        }
    }
}