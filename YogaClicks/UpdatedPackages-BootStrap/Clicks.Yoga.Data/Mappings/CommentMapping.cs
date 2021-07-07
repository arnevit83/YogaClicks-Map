using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    public class CommentMapping : EntityMapping<Comment>
    {
        public CommentMapping()
        {
            HasRequired(e => e.Owner).WithMany().Map(a => a.MapKey("OwnerId")).WillCascadeOnDelete(false);
            HasRequired(e => e.ActorHandle).WithMany().Map(a => a.MapKey("ActorHandleId")).WillCascadeOnDelete(false); ;
            Property(e => e.Content).IsRequired();
        }
    }
}