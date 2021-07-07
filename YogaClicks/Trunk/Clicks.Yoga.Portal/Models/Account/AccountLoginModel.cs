namespace Clicks.Yoga.Portal.Models.Account
{
    public class AccountLoginModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Destination { get; set; }
        public string Validation { get; set; }
        public bool Persist { get; set; }
    }
}