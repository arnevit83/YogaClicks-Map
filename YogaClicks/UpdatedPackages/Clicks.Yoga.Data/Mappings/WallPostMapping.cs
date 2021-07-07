using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class WallPostMapping : EntityMapping<WallPost>
    {
        public WallPostMapping()
        {
            HasRequired(e => e.Owner).WithMany().Map(a => a.MapKey("OwnerId")).WillCascadeOnDelete(false);
            HasRequired(e => e.ActorHandle).WithMany().Map(a => a.MapKey("ActorHandleId")).WillCascadeOnDelete(false);
            HasRequired(e => e.Wall).WithMany(e => e.Posts).Map(a => a.MapKey("WallId"));
            Property(e => e.Content).IsRequired();
            HasMany(e => e.Comments).WithMany().Map(a => a.MapLeftKey("PostId").MapRightKey("CommentId").ToTable("WallPostComment"));
            HasMany(e => e.Resources).WithMany().Map(a => a.MapLeftKey("PostId").MapRightKey("ResourceId").ToTable("WallPostResource"));
            HasOptional(e => e.SharedStory).WithMany().Map(a => a.MapKey("SharedStoryId"));
        }
    }
}