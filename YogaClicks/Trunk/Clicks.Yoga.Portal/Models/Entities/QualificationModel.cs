using Clicks.Yoga.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class QualificationModel : EntityModel<Qualification>
    {
        public QualificationModel(Qualification entity) : base(entity) { }

        public string Name { get; set; }

        public override void Populate(Qualification entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }
    }
}