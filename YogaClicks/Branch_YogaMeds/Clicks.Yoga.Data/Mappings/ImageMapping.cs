using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class ImageMapping : PrincipalEntityMapping<Image>
    {
        public ImageMapping()
        {
            Property(e => e.Path).IsRequired();
            Property(e => e.Guid).IsRequired();
            HasRequired(e => e.Type).WithMany().Map(a => a.MapKey("TypeId"));
            Ignore(e => e.Source);
        }
    }
}