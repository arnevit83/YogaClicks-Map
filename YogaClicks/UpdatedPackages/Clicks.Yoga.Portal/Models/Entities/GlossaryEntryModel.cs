using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class GlossaryEntryModel : EntityModel<GlossaryEntry>
    {
        public GlossaryEntryModel(GlossaryEntry entity) : base(entity) {}

        public string Term { get; set; }

        public string Definition { get; set; }

        public override void Populate(GlossaryEntry entity)
        {
            Id = entity.Id;
            Term = entity.Term;
            Definition = entity.Definition;
        }
    }
}