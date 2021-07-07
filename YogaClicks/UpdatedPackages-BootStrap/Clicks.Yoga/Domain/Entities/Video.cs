using System.Collections.Generic;

namespace Clicks.Yoga.Domain.Entities
{
    public class Video : PrincipalEntity
    {
        public static readonly string[] LengthOptions = { "30 minutes", "60 minutes", "90 minutes", "120 minutes", "150 minutes", "180 minutes" };

        public Video()
        {
            Associations = new List<VideoAssociation>();
        }

        public string Identifier { get; set; }

        public string Description { get; set; }

        public string Length { get; set; }

        public virtual AbilityLevel AbilityLevel { get; set; }

        public virtual VideoType Type { get; set; }

        public virtual ICollection<VideoAssociation> Associations { get; private set; }

        public bool IsClass { get; set; }

        public string EmbedHtml
        {
            get { return string.Format(Type.EmbedHtml, Identifier, "{0}", "{1}"); }
        }
    }
}
