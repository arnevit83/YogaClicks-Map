using System.Collections.Generic;
using System.Web.Mvc;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.Account
{
    public class AccountSettingsModel
    {
        public AccountSettingsModel()
        {
            NotificationCategories = new NotificationCategoryChooserModel();
        }

        public AccountSettingsModel(User user, INotificationService notificationService)
        {
            var preferences = user.NotificationPreferences;

            ProfileId = user.Profile.Id;
            CurrentEmailAddress = user.EmailAddress;
            NewsletterOptOut  =  user.Profile.NewsletterOptOut;
            ProfilePublic = user.Profile.Public;
            ThirdPartyOptOut = user.Profile.ThirdPartyOptOut;
            NotificationDigestTimescale = preferences != null && preferences.EmailDigestEnabled ? preferences.EmailDigestTimescale.Tag : null;
            NotificationCategories = new NotificationCategoryChooserModel(notificationService.GetSelectedDigestNotificationCategories(user));
            SharingPermitted = (user.PrivacyPreferences ?? PrivacyPreferences.Default).SharingPermitted;
        }

        public int ProfileId { get; set; }

        public bool UserHasPassword { get; set; }

        public bool UserHasConnectedFacebook { get; set; }

        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }

        public string NewPasswordConfirmation { get; set; }

        public string CurrentEmailAddress { get; set; }

        public string NewEmailAddress { get; set; }

        public string NewEmailAddressConfirmation { get; set; }

        public bool ProfilePublic { get; set; }

        public bool SharingPermitted { get; set; }

        public bool NewsletterOptOut { get; set; }

        public bool ThirdPartyOptOut { get; set; }

        public string NotificationDigestTimescale { get; set; }

        public NotificationCategoryChooserModel NotificationCategories { get; private set; }

        public IList<SelectListItem> ProfilePublicOptions
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem { Value = "true", Text = "is" },
                    new SelectListItem { Value = "false", Text = "isn't" },
                };
            }
        }

        public IList<SelectListItem> SharingPermittedOptions
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem { Value = "true", Text = "can" },
                    new SelectListItem { Value = "false", Text = "can't" },
                };
            }
        }

        public IList<SelectListItem> NotificationDigestTimescaleOptions
        {
            get
            {
                return new List<SelectListItem>
                {
                    new SelectListItem { Value = "Week", Text = "once a week" },
                    new SelectListItem { Value = "Month", Text = "once a month" },
                    new SelectListItem { Value = "Day", Text = "once a day" },
                    new SelectListItem { Value = "", Text = "never" }
                };
            }
        }

        public AccountSettingsModel PopuplateMetadata(IAccountService accountService, INotificationService notificationService)
        {
            UserHasPassword = accountService.CurrentUserHasPasswordLogin();
            UserHasConnectedFacebook = accountService.CurrentUserHasFederatedLogin("Facebook");
            NotificationCategories.PopulateMetadata(notificationService.GetDigestNotificationCategories());
            return this;
        }
    }
}
