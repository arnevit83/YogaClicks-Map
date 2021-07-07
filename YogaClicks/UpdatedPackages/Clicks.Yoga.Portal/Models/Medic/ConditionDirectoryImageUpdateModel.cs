using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;

namespace Clicks.Yoga.Portal.Models.Medic
{
    public class ConditionDirectoryImageUpdateModel
    {
         public ConditionDirectoryImageUpdateModel()
        {
        }

         public ConditionDirectoryImageUpdateModel(Condition condition)
        {
            ConditionDirectoryImage = new ConditionDirectoryImageEditorModel(condition.DirectoryImage);
            ConditionId = condition.Id;
        }
         public ConditionDirectoryImageEditorModel ConditionDirectoryImage { get; set; }

         public int ConditionId { get; set; }

        public void PopulateEntity(
            Condition condition,
            IImageService imageService)
        {
            condition.DirectoryImage = ConditionDirectoryImage.PopulateEntity(() => condition.DirectoryImage, imageService);
        }
    }
}