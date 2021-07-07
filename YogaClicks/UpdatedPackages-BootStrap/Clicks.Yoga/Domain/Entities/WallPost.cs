using System;
using System.Collections.Generic;
using Clicks.Yoga.Permissions.Providers;

namespace Clicks.Yoga.Domain.Entities
{
    public class WallPost : Entity, ICommentable, ISecurable
    {
        public WallPost()
        {
            Comments = new List<Comment>();
            Resources = new List<MediaResource>();
        }

        public virtual User Owner { get; set; }

        public virtual EntityHandle ActorHandle { get; set; }

        public virtual Wall Wall { get; set; }

        public string Content { get; set; }

        public virtual ICollection<MediaResource> Resources { get; private set; } 

        public virtual ICollection<Comment> Comments { get; private set; }

        public virtual NewsStory SharedStory { get; set; }

        public Type GetCommentPermissionProviderType()
        {
            return typeof(WallPostCommentPermissionProvider);
        }

        public int? OwnerId
        {
            get { return Owner == null ? (int?)null : Owner.Id; }
        }

        public EntityReference? GetReferencedEntity()
        {
            return null;
        }
    }
}