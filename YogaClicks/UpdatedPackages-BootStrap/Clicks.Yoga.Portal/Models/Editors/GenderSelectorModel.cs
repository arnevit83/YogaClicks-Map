using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Editors
{
    public class GenderSelectorModel : SingleEntitySelectorModel<Gender, GenderModel>
    {
        public GenderSelectorModel() {}

        public GenderSelectorModel(Gender entity) : base(entity) {}
    }
}