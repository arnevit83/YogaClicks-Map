namespace Clicks.Yoga.Portal.Models.Account
{
    public class AccountPasswordResetModel
    {
        public AccountPasswordResetModel()
        {
        }

        public AccountPasswordResetModel(string key)
        {
            Key = key;
        }

        public string Key { get; set; }

        public string Password { get; set; }

        public string PasswordConfirmation { get; set; }
    }
}