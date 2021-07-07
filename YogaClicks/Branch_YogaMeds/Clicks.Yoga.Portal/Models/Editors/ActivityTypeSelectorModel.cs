using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Editors
{
    public class ActivityTypeSelectorModel : SingleEntitySelectorModel<ActivityType, ActivityTypeModel>
    {
        public ActivityTypeSelectorModel() {}

        public ActivityTypeSelectorModel(ActivityType entity) : base(entity)
        {
            ExcludeClass = true;
        }

        bool ExcludeClass { get; set; }

        public override void PopulateMetadata(System.Collections.Generic.IEnumerable<ActivityType> entities)
        {
            if (ExcludeClass)
            {
                base.PopulateMetadata(entities.Where(t => !t.IsClass));
            }
            else
            {
                base.PopulateMetadata(entities);
            }
        }
    }
}