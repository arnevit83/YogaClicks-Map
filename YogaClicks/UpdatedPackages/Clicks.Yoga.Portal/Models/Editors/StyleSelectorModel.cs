using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities.Summaries;

namespace Clicks.Yoga.Portal.Models.Editors
{
    public class StyleSelectorModel : SingleEntitySelectorModel<Style, NamedSummaryModel<Style>>
    {
        public StyleSelectorModel() { }

        public StyleSelectorModel(Style entity) : base(entity) {}

        public StyleSelectorModel(Style entity, IEnumerable<Style> options) : base(entity, options) {}
    }
}