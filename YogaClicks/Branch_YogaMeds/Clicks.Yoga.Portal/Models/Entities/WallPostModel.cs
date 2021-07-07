using System;
using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class WallPostModel : EntityModel<WallPost>, ISecurable
    {
        public WallPostModel(WallPost entity) : base(entity) {}

        public EntityHandleModel Actor { get; private set; }

        public string Content { get; private set; }

        public DateTime Time { get; private set; }

        public IList<MediaResourceModel> Resources { get; private set; }

        public IList<CommentModel> Comments { get; private set; }

        public int? OwnerId { get; private set; }

        public override void Populate(WallPost entity)
        {
            Actor = new EntityHandleModel(entity.ActorHandle);
            Content = entity.Content;
            Time = entity.CreationTime;
            OwnerId = entity.OwnerId;
            Resources = new List<MediaResourceModel>();
            Comments = new List<CommentModel>();

            foreach (var resource in entity.Resources)
            {
                Resources.Add(new MediaResourceModel(resource));
            }

            foreach (var comment in entity.Comments.Where(c => c.ActorHandle.Active))
            {
                Comments.Add(new CommentModel(comment));
            }
        }
    }
}