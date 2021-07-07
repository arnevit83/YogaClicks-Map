using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities.Summaries
{
    public class ProfileSummaryModel : PrincipalEntityModel<Profile>, ISecurable
    {
        public ProfileSummaryModel() { }

        public ProfileSummaryModel(Profile entity) : base(entity) { }

        public string Name { get; private set; }

        public ImageModel Image { get; private set; }

        public LocationModel Location { get; private set; }

        public override void Populate(Profile entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Image = new ImageModel(entity.Image);
            Location = new LocationModel(entity.Location);
        }
    }
}