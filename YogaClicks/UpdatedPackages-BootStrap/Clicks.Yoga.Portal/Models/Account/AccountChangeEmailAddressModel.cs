namespace Clicks.Yoga.Portal.Models.Account
{
    public class AccountChangeEmailAddressModel
    {
        public string CurrentEmailAddress { get; set; }

        public string NewEmailAddress { get; set; }

        public string NewEmailAddressConfirmation { get; set; }
    }
}