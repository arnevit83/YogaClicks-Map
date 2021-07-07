using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data.Mappings
{
    internal class ProfileQuestionMapping : EntityMapping<ProfileQuestion>
    {
        public ProfileQuestionMapping()
        {
            Property(e => e.Text).IsRequired().HasMaxLength(100);
            Property(e => e.DisplayOrder).IsRequired();
        }
    }
}