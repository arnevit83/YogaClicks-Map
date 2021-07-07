using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class RequestTypeMapping : EntityMapping<RequestType>
    {
        public RequestTypeMapping()
        {
            Property(e => e.Tag).IsRequired();
            Property(e => e.Message).IsRequired();
            Property(e => e.Icon).IsRequired();
            Property(e => e.CompletionHandlerTypeName).IsRequired();
            HasOptional(e => e.Category).WithMany(e => e.RequestTypes).Map(a => a.MapKey("CategoryId"));
            Property(e => e.IsFriend).IsRequired();
        }
    }
}