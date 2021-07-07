using System;
using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public class PoseUserImageService : ServiceBase, IPoseUserImageService
    {
        public PoseUserImageService(
            IEntityContext entityContext,
            ISecurityContext securityContext)
            : base(entityContext, securityContext) {}

        public IList<PoseUserImages> GetPoseUserImage(int id)
        {
            return EntityContext.PoseUserImages.Where(e => e.Pose.Id == id).ToList();
        }

        public IList<PoseUserImages> GetPoseUserImages()
        {
            return EntityContext.PoseUserImages.Select(x => x).ToList();
        }

        public PoseUserImages GetPoseImage(int id)
        {
            return EntityContext.PoseUserImages.Get(x => x.Id == id);
        }

        
        public void AddImage(PoseUserImages story)
        {
            EntityContext.PoseUserImages.Add(story);
        }
        public void RemoveImage(PoseUserImages story)
        {
            EntityContext.PoseUserImages.Remove(story);
        }
   

    }
}