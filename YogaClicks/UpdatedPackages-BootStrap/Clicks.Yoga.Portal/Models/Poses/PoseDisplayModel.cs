using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Poses
{
    public class PoseDisplayModel
    {
        public PoseDisplayModel(Pose style, IEnumerable<PoseCategory> categories, IEnumerable<Pose> poses)
        {
            Pose = new PoseDetailModel(style);
            Categories = new List<PoseCategoryModel>();
            Poses = new List<PoseModel>();

            foreach (var c in categories)
            {
                Categories.Add(new PoseCategoryModel(c));
            }

            foreach (var p in poses)
            {
                Poses.Add(new PoseModel(p));
            }
        }

        public PoseDetailModel Pose { get; private set; }

        public IList<PoseCategoryModel> Categories { get; private set; }

        public IList<PoseModel> Poses { get; private set; }
    }
}