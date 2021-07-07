using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public interface IPoseService
    {
        Pose GetPose(int id);
        Pose GetPoseForDisplay(int id);
        IList<Pose> GetPoses();
        IList<Pose> GetPosesByCategory(int id);
        IList<PoseCategory> GetPoseCategories();
        PoseCategory GetPoseCategory(int id);
    }
}