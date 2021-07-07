using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;

namespace Clicks.Yoga.Portal.Models.Poses
{
    public class UpdateUserPoseImagesUpdateModel
    {
        public UpdateUserPoseImagesUpdateModel()
        {
            
        }

         public UpdateUserPoseImagesUpdateModel(int poses)
         {
             //ConditionDirectoryImage = new ConditionDirectoryImageEditorModel(condition.DirectoryImage);
             UpdateUserPoseImages = new UpdateUserPoseImagesEditorModel();
             PosesId = poses;
         }
         public UpdateUserPoseImagesEditorModel UpdateUserPoseImages { get; set; }

         public int PosesId { get; set; }

        public void PopulateEntity(
            PoseUserImages poseImage,
            IImageService imageService)
        {

      
            poseImage.Image = UpdateUserPoseImages.PopulateEntity(() => poseImage.Image, imageService);
         




        }
    }
}