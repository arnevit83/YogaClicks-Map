using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class NewsStoryMapping : EntityMapping<NewsStory>
    {
        public NewsStoryMapping()
        {
            HasRequired(e => e.Type).WithMany(e => e.Stories).Map(a => a.MapKey("TypeId"));
            HasRequired(e => e.Subject).WithMany().Map(a => a.MapKey("SubjectHandleId")).WillCascadeOnDelete(false);
            HasOptional(e => e.Actor).WithMany().Map(a => a.MapKey("ActorHandleId")).WillCascadeOnDelete(false);
            HasOptional(e => e.Target).WithMany().Map(a => a.MapKey("TargetHandleId")).WillCascadeOnDelete(false);
            Property(e => e.EntityId);
            HasOptional(e => e.EntityType).WithMany().Map(a => a.MapKey("EntityTypeId"));
            Property(e => e.Text).HasMaxLength(4000);
            HasOptional(e => e.Inverse).WithOptionalDependent().Map(a => a.MapKey("InverseId"));
            HasMany(e => e.Resources).WithMany().Map(a => a.MapLeftKey("StoryId").MapRightKey("ResourceId").ToTable("NewsStoryResource"));
            HasMany(e => e.Comments).WithMany().Map(a => a.MapLeftKey("StoryId").MapRightKey("CommentId").ToTable("NewsStoryComment"));
            HasOptional(e => e.ShareOriginal).WithMany().Map(a => a.MapKey("ShareOriginalId"));
        }
    }
}