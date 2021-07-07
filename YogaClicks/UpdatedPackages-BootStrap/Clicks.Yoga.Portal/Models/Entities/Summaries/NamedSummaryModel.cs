using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities.Summaries
{
    public class NamedSummaryModel<TEntity> : EntityModel<TEntity> where TEntity : class, INamed
    {
        public NamedSummaryModel() {}

        public NamedSummaryModel(TEntity entity) : base(entity) {}

        public string Name { get; private set; }

        public bool Stubbed { get; set; }

        public override void Populate(TEntity entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }
    }

    public class NamedSummaryModel : NamedSummaryModel<INamed>
    {
        public NamedSummaryModel() {}

        public NamedSummaryModel(INamed entity, bool stubbed = false) : base(entity) 
        {
            Stubbed = stubbed;
        }
    }
}