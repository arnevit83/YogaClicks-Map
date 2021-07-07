using Clicks.Yoga.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Medic
{
    public class ConditionsModel
    {
        public ConditionsModel(IEnumerable<Condition> conditions)
        {
            Conditions = new List<ConditionModel>();

            foreach (var condition in conditions)
            {
                Conditions.Add(new ConditionModel(condition));
            }
        }

        public IList<ConditionModel> Conditions { get; private set; }
    }
}