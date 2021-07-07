using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class WallModel : EntityModel<Wall>
    {
        public WallModel(Wall entity) : base(entity) {}

        public override void Populate(Wall entity)
        {
        }
    }
}