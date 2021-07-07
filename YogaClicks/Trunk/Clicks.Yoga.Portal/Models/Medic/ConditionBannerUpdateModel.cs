using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Medic
{
    public class ConditionBannerUpdateModel
    {
         public ConditionBannerUpdateModel()
        {
        }

         public ConditionBannerUpdateModel(Condition condition)
        {
            ConditionBanner = new ConditionBannerEditorModel(condition.ProfileBanner);
            ConditionId = condition.Id;
        }
         public ConditionBannerEditorModel ConditionBanner { get; set; }

         public int ConditionId { get; set; }

        public void PopulateEntity(
            Condition condition,
            IImageService imageService)
        {
            condition.ProfileBanner = ConditionBanner.PopulateEntity(() => condition.ProfileBanner, imageService);
        }
    }
}