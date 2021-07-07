using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class RequestMapping : EntityMapping<Request>
    {
        public RequestMapping()
        {
            HasRequired(e => e.User).WithMany().Map(a => a.MapKey("UserId"));
            HasRequired(e => e.Type).WithMany().Map(a => a.MapKey("TypeId"));
            HasRequired(e => e.ActorHandle).WithMany().Map(a => a.MapKey("ActorHandleId"));
            HasOptional(e => e.SubjectHandle).WithMany().Map(a => a.MapKey("SubjectHandleId"));
            HasOptional(e => e.ContextHandle).WithMany().Map(a => a.MapKey("ContextHandleId"));
            Property(e => e.EntityId);
            Property(e => e.Completed).IsRequired();
            Property(e => e.CompletionTime);
        }
    }
}