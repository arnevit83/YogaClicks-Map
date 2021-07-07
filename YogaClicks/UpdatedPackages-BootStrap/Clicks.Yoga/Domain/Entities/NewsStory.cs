using System;
using System.Collections.Generic;
using Clicks.Yoga.Permissions.Providers;

namespace Clicks.Yoga.Domain.Entities
{
    public class NewsStory : Entity, ICommentable, IFanable
    {
        public NewsStory()
        {
            Resources = new List<MediaResource>();
            Comments = new List<Comment>();
        }

        public string Name { get; set; }
        public virtual Image Image { get; set; }
        public virtual Location Location { get; set; }
        string IEntityHandle.Location
        {
            get { return Location != null ? Location.Name : null; }
        }
        public bool Active { get; set; }

        public virtual NewsStoryType Type { get; set; }

        public virtual EntityHandle Subject { get; set; }

        public virtual EntityHandle Actor { get; set; }

        public virtual EntityHandle Target { get; set; }

        public int? EntityId { get; set; }

        public virtual EntityType EntityType { get; set; }

        public string Text { get; set; }

        public virtual NewsStory Inverse { get; set; }

        public virtual ICollection<MediaResource> Resources { get; private set; }

        public virtual ICollection<Comment> Comments { get; private set; }

        public virtual NewsStory ShareOriginal { get; set; }

        public Type GetCommentPermissionProviderType()
        {
            return typeof(NewsStoryCommentPermissionProvider);
        }

        public virtual User Owner
        {
            get { return Actor == null ? null : Actor.Owner; }
        }

        public EntityReference? GetReferencedEntity()
        {
            if(EntityId != null)
                return new EntityReference(EntityId.Value, EntityType.Name);

            return null;
        }

        public int? OwnerId
        {
            get { return Owner == null ? (int?)null : Owner.Id; }
        }
    }
}