namespace Clicks.Yoga.Portal.Models.Account
{
    public class AccountChangePasswordModel
    {
        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }

        public string NewPasswordConfirmation { get; set; }
    }
}