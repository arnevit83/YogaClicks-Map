using Clicks.Yoga.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Medic
{
    public class ConditionStudiesModel
    {
        public ConditionStudiesModel() { }

        public ConditionStudiesModel(Condition condition)
        {
            Studies = new StudiesModel(condition.Studies);
            Id = condition.Id;
        }

        public StudiesModel Studies { get; set; }
        public int Id { get; set; }
    }
}