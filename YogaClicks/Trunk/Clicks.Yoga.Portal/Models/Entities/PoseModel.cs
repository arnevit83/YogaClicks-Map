using System;
using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Web;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class PoseModel : EntityModel<Pose>
    {
        public PoseModel(Pose pose) : base(pose) {}

        public string EnglishName { get; set; }

        public string SanskritName { get; set; }

        public IList<string> Roots { get; set; }

        public string Pronounciation { get; set; }

        public IList<string> Instructions { get; set; }

        public IList<string> Effects { get; set; }

        public IList<string> Tips { get; set; }

        public IList<string> Indications { get; set; }

        public IList<string> Contraindications { get; set; }

        public AbilityLevelModel AbilityLevel { get; set; }

        public ImageModel Image { get; set; }

        public VideoModel Video { get; set; }

        public int CategoryId { get; set; }

        public override void Populate(Pose entity)
        {
            Id = entity.Id;
            EnglishName = entity.EnglishName;
            SanskritName = entity.SanskritName;
            Roots = SplitLines(entity.Roots);
            Pronounciation = entity.Pronounciation;
            Instructions = SplitLines(entity.Instructions);
            Effects = SplitLines(entity.Effects);
            Tips = SplitLines(entity.Tips);
            Indications = SplitLines(entity.Indications);
            Contraindications = SplitLines(entity.Contraindications);
            CategoryId = entity.Category.Id;
            AbilityLevel = new AbilityLevelModel(entity.AbilityLevel);
            if (entity.Image != null) Image = new ImageModel(entity.Image);
            if (entity.Video != null) Video = new VideoModel(entity.Video);
        }

        public string UrlSlug
        {
            get { return WebUtility.UrlSlug(EnglishName); }
        }

        private IList<string> SplitLines(string source)
        {
            return source.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None).ToList();
        }
    }
}