using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Medic
{
    public class WhatTheScienceSaysModel : EntityModel<WhatTheScienceSays>
    {
        public WhatTheScienceSaysModel() { }

        public WhatTheScienceSaysModel(WhatTheScienceSays entity) : base(entity) { }

        public string Description { get; set; }

        public List<TherapyTypeModel> TherapyTypes { get; set; }

        public override void Populate(WhatTheScienceSays entity)
        {
            Id = entity.Id;
            Description = entity.Description;

            var therapyTypes = new List<TherapyTypeModel>();

            foreach (var therapyType in entity.TherapyTypes)
            {
                therapyTypes.Add(new TherapyTypeModel(therapyType));
            }

            TherapyTypes = therapyTypes;
        }
    }
}