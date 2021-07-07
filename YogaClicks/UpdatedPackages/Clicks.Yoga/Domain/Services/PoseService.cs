using System;
using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public class PoseService : ServiceBase, IPoseService
    {
        public PoseService(
            IEntityContext entityContext,
            ISecurityContext securityContext)
            : base(entityContext, securityContext) {}

        public Pose GetPose(int id)
        {
            return EntityContext.Poses.Get(e => e.Id == id);
        }

        public Pose GetPoseForDisplay(int id)
        {
            var pose = GetPose(id);

            if (pose == null) throw new UnknownEntityException();

            return pose;
        }

        public IList<Pose> GetPoses()
        {
            return EntityContext.Poses
                .Include(e => e.Category)
                .Include(e => e.Image)
                .Include(e => e.AbilityLevel)
                .OrderBy(e => e.EnglishName)
                .ToList();
        }

        public IList<Pose> GetPosesByCategory(int id)
        {
            return EntityContext.Poses.Where(e => e.Category.Id == id).OrderBy(e => e.EnglishName).ToList();
        }

        public IList<PoseCategory> GetPoseCategories()
        {
            return EntityContext.PoseCategories.OrderBy(e => e.Name).ToList();
        }

        public PoseCategory GetPoseCategory(int id)
        {
            return EntityContext.PoseCategories.Get(id);
        }
    }
}