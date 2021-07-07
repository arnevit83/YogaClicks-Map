using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Clicks.Yoga.Portal.Models.Medic
{
    public class ConditionDirectoryImageEditorModel : ImageEditorModel
    {
         public ConditionDirectoryImageEditorModel() {}

         public ConditionDirectoryImageEditorModel(Image image) : base(image) { }
    }
}