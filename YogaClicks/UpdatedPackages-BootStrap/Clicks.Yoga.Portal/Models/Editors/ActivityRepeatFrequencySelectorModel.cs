using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Editors
{
    public class ActivityRepeatFrequencySelectorModel : SingleEntitySelectorModel<ActivityRepeatFrequency, ActivityRepeatFrequencyModel>
    {
        public ActivityRepeatFrequencySelectorModel() { }

        public ActivityRepeatFrequencySelectorModel(ActivityRepeatFrequency entity) : base(entity) { }

        public override void PopulateMetadata(IEnumerable<ActivityRepeatFrequency> entities)
        {
            base.PopulateMetadata(entities.Where(e => !e.IsSingle));
        }
    }
}