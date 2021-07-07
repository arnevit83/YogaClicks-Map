using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services.Interfaces;
using Clicks.Yoga.Portal.Models.Entities.Summaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Editors
{
    [DataContract]
    public class ConditionChooserModel : MultipleEntitySelectorModel<Condition, NamedSummaryModel<Condition>>
    {
        public ConditionChooserModel() { }

        public ConditionChooserModel(IEnumerable<Condition> entities) : base(entities) { }

        public ConditionChooserModel(IEnumerable<Condition> selections, IEnumerable<Condition> options) : base(selections, options) { }

        public void PopulateCollection(ICollection<Condition> collection, IMedicService medicService)
        {
            PopulateCollection(collection, medicService.GetCondition);
        }
    }
}