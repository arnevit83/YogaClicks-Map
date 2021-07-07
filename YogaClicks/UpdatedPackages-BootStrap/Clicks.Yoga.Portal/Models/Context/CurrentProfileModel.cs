using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Context
{
    public class CurrentProfileModel
    {
        public CurrentProfileModel(Profile profile)
        {
            Id = profile.Id;
            Professional = profile.Professional;
            Name = profile.Name;
            Image = new ImageModel(profile.Image);
        }

        public int Id { get; set; }

        public bool Professional { get; set; }

        public string Name { get; set; }

        public ImageModel Image { get; private set; }
    }
}