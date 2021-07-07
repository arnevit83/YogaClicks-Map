using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Poses
{
    public class PoseCategoryDisplayModel
    {
        public PoseCategoryDisplayModel(PoseCategory category, IEnumerable<Pose> poses)
        {
            Category = new PoseCategoryModel(category);

            Poses = new List<PoseModel>();

            foreach (var pose in poses)
            {
                Poses.Add(new PoseModel(pose));
            }
        }

        public PoseCategoryModel Category { get; set; }

        public IList<PoseModel> Poses { get; private set; }
    }
}