using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class VideoModel : PrincipalEntityModel<Video>
    {
        public VideoModel(Video entity) : base(entity) {}

        public VideoTypeModel Type { get; private set; }

        public string Identifier { get; private set; }

        public string Description { get; private set; }

        public string Length { get; private set; }

        public AbilityLevelModel AbilityLevel { get; private set; }

        public string EmbedHtml { get; private set; }

        public bool IsClass { get; private set; }

        public string GetEmbedHtml(int width, int height)
        {
            return string.Format(EmbedHtml, width, height);
        }

        public override void Populate(Video entity)
        {
            Id = entity.Id;
            Type = new VideoTypeModel(entity.Type);
            Identifier = entity.Identifier;
            Description = entity.Description;
            Length = entity.Length;
            AbilityLevel = new AbilityLevelModel(entity.AbilityLevel);
            EmbedHtml = entity.EmbedHtml;
            IsClass = entity.IsClass;
        }
    }
}