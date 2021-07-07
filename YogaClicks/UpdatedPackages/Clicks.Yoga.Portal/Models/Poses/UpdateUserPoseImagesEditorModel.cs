using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Poses
{
    public class UpdateUserPoseImagesEditorModel : ImageEditorModel
    {
         public UpdateUserPoseImagesEditorModel() {}

         public UpdateUserPoseImagesEditorModel(Image image) : base(image) { }
    }
}