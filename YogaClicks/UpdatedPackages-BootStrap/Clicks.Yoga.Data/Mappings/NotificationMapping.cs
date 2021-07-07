using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class NotificationMapping : EntityMapping<Notification>
    {
        public NotificationMapping()
        {
            HasRequired(e => e.Type).WithMany().Map(a => a.MapKey("TypeId"));
            Property(e => e.Message);
            HasOptional(e => e.ActorHandle).WithMany().Map(a => a.MapKey("ActorHandleId")).WillCascadeOnDelete(false);
            HasOptional(e => e.SubjectHandle).WithMany().Map(a => a.MapKey("SubjectHandleId")).WillCascadeOnDelete(false);
            HasOptional(e => e.ContextHandle).WithMany().Map(a => a.MapKey("ContextHandleId")).WillCascadeOnDelete(false);
            Property(e => e.EntityId);
            HasOptional(e => e.EntityType).WithMany().Map(a => a.MapKey("EntityTypeId"));
            Property(e => e.Broadcast).IsRequired();
        }
    }
}