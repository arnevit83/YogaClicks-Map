using System;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class CommentModel : EntityModel<Comment>, ISecurable
    {
        public CommentModel(Comment entity) : base(entity) {}

        public EntityHandleModel Actor { get; private set; }

        public string Content { get; private set; }

        public DateTime Time { get; private set; }

        public int? OwnerId { get; private set; }

        public override void Populate(Comment entity)
        {
            Actor = new EntityHandleModel(entity.ActorHandle);
            Content = entity.Content;
            Time = entity.CreationTime;
            OwnerId = entity.OwnerId;
        }
    }
}