using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Poses
{
    public class PoseDirectoryModel
    {
        public PoseDirectoryModel(IEnumerable<Pose> poses)
        {
            Poses = new List<PoseModel>();

            foreach (var pose in poses.OrderBy(e => e.EnglishName))
            {
                Poses.Add(new PoseModel(pose));
            }
        }

        public PoseDirectoryModel(IEnumerable<Pose> poses, Pose pose)
            : this(poses)
        {
            Pose = new PoseModel(pose);
        }

        public PoseDirectoryModel(IEnumerable<Pose> poses, Pose pose, bool sanskritEnabled)
            : this(poses, pose)
        {
            Pose = new PoseModel(pose);
            SanskritEnabled = sanskritEnabled;
        }

        public IList<PoseModel> Poses { get; private set; }

        public PoseModel Pose { get; private set; }

        public bool SanskritEnabled { get; private set; }
    }
}