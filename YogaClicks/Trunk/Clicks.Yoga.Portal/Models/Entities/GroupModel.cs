using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class GroupModel : PrincipalEntityModel<Group>
    {
        public GroupModel(Group entity) : base(entity) {}

        public string Name { get; private set; }

        public string Description { get; private set; }

        public EntityHandleModel PromoterHandle { get; private set; }

        public ImageModel Image { get; private set; }

        public ImageModel ProfileBanner { get; private set; }

        public bool Professional { get; private set; }

        public bool ProfessionsRestricted { get; private set; }

        public bool Public { get; private set; }

        public WallModel Wall { get; private set; }

        public override void Populate(Group entity)
        {
            Name = entity.Name;
            Description = entity.Description;
            PromoterHandle = new EntityHandleModel(entity.PromoterHandle);
            Image = new ImageModel(entity.Image);
            ProfileBanner = new ImageModel(entity.ProfileBanner);
            Professional = entity.Professional;
            ProfessionsRestricted = entity.ProfessionsRestricted;
            Wall = new WallModel(entity.Wall);
            Public = entity.Public;
        }
    }
}