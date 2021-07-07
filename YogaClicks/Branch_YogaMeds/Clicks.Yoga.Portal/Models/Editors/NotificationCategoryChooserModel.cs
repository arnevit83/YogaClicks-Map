using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Editors
{
    public class NotificationCategoryChooserModel : MultipleEntitySelectorModel<NotificationCategory, NotificationCategoryModel>
    {
        public NotificationCategoryChooserModel() {}

        public NotificationCategoryChooserModel(IEnumerable<NotificationCategory> entities) : base(entities) {}
    }
}