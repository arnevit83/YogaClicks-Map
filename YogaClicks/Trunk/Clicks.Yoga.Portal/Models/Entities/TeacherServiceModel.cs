using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class TeacherServiceModel : EntityModel<TeacherService>
    {
        public TeacherServiceModel() {}

        public TeacherServiceModel(TeacherService entity) : base(entity) {}

        public string Name { get; set; }

        public int DisplayOrder { get; set; }

        public override void Populate(TeacherService entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            DisplayOrder = entity.DisplayOrder;
        }
    }
}