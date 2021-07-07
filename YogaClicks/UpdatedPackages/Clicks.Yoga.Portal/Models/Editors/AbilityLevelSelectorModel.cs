using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Editors
{
    public class AbilityLevelSelectorModel : SingleEntitySelectorModel<AbilityLevel, AbilityLevelModel>
    {
        public AbilityLevelSelectorModel() { }

        public AbilityLevelSelectorModel(AbilityLevel entity) : base(entity) { }

    }
}