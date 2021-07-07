using System;

namespace Clicks.Yoga.Domain.Entities
{
    public class MediaResource : Entity
    {
        public virtual MediaResourceType Type { get; set; }

        public string Uri { get; set; }

        public string Identifier { get; set; }

        public int? EntityId { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public virtual Image Image { get; set; }

        public DateTime? ExpiryTime { get; set; }
    }
}