using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class TeacherPlacementTeacherModel : ISecurable
    {
        public TeacherPlacementTeacherModel(Teacher entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Philosophy = entity.Philosophy;
            if (entity.Owner != null) OwnerId = entity.Owner.Id;
            if (entity.Location != null) Location = new LocationModel(entity.Location);
            if (entity.Image != null) Image = new ImageModel(entity.Image);
        }

        public TeacherPlacementTeacherModel(TeacherModel model)
        {
            Id = model.Id;
            Name = model.Name;
            Philosophy = model.Philosophy;
            Location = model.Location;
            Image = model.Image;

            var securable = (ISecurable)model;

            OwnerId = securable.OwnerId;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Philosophy { get; set; }

        public LocationModel Location { get; set; }

        public ImageModel Image { get; set; }

        public int? OwnerId { get; private set; }
    }
}
