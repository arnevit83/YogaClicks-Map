using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Medic
{
    public class TherapyTypeModel : EntityModel<TherapyType>
    {
        public TherapyTypeModel() { }

        public TherapyTypeModel(TherapyType entity) : base(entity) { }

        public string Name { get; set; }

        public bool Active { get; set; }

        public override void Populate(TherapyType entity)
        {
            Id = entity.Id;
            Name = entity.Name.Replace(Environment.NewLine, string.Empty);
        }
    }
}