using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class TeacherAccreditationModel : EntityModel<TeacherAccreditation>
    {
        public TeacherAccreditationModel(TeacherAccreditation entity) : base(entity) {}

        public AccreditationBodyModel AccreditationBody { get; set; }

        public override void Populate(TeacherAccreditation entity)
        {
            Id = entity.Id;
            AccreditationBody = new AccreditationBodyModel(entity.AccreditationBody);
        }
    }
}