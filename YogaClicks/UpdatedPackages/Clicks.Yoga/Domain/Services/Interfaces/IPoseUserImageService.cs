using System;
using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public interface IPoseUserImageService
    {

        IList<PoseUserImages> GetPoseUserImage(int id);

     
        PoseUserImages GetPoseImage(int id);

        IList<PoseUserImages> GetPoseUserImages();



        void AddImage(PoseUserImages story);

        void RemoveImage(PoseUserImages story);

    }
}
