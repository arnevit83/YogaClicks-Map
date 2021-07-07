using System.Collections.Generic;
using System.Runtime.Serialization;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Entities.Summaries;

namespace Clicks.Yoga.Portal.Models.Editors
{
    [DataContract]
    public class SignUpStyleChooserModel : MultipleEntitySelectorModel<Style, NamedSummaryModel<Style>>
    {
        public SignUpStyleChooserModel() {}

        public SignUpStyleChooserModel(IEnumerable<Style> entities) : base(entities) {}

        public SignUpStyleChooserModel(IEnumerable<Style> selections, IEnumerable<Style> options) : base(selections, options) { }

        public void PopulateCollection(ICollection<Style> collection, IStyleService styleService)
        {
            PopulateCollection(collection, styleService.GetStyle);
        }
    }
}