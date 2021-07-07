using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.Account
{
    public class AccountNotificationPreferencesPartialModel
    {
        public AccountNotificationPreferencesPartialModel()
        {
            NotificationCategories = new NotificationCategoryChooserModel();
        }

        public AccountNotificationPreferencesPartialModel(IEnumerable<NotificationCategory> categories)
        {
            NotificationCategories = new NotificationCategoryChooserModel(categories);
        }

        public NotificationCategoryChooserModel NotificationCategories { get; private set; }

        public AccountNotificationPreferencesPartialModel PopuplateMetadata(INotificationService notificationService)
        {
            NotificationCategories.PopulateMetadata(notificationService.GetDigestNotificationCategories());
            return this;
        }
    }
}