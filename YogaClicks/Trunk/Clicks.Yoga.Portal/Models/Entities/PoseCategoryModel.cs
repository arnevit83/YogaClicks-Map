using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class PoseCategoryModel : EntityModel<PoseCategory>
    {
        public PoseCategoryModel(PoseCategory entity) : base(entity) {}

        public string Name { get; set; }

        public int Count { get; set; }

        public override void Populate(PoseCategory entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Count = entity.Poses.Count;
        }
    }
}