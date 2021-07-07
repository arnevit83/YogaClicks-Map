using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Medic
{
    public class ConditionWTSSModel
    {

        public ConditionWTSSModel() { }

        public ConditionWTSSModel(Condition condition)
        {
            Id = condition.Id;
            WhatTheScienceSayses = new WhatTheScienceSaysesModel(condition.WhatTheScienceSayses);
        }

        public int Id { get; set; }
        public WhatTheScienceSaysesModel WhatTheScienceSayses { get; set; }

        public List<TherapyTypeModel> TherapyTypes { get; set; }
    }
}