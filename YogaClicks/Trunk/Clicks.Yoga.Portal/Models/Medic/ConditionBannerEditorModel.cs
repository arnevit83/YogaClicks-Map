using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Medic
{
    public class ConditionBannerEditorModel : ImageEditorModel
    {
         public ConditionBannerEditorModel() {}

         public ConditionBannerEditorModel(Image image) : base(image) { }
    }
}