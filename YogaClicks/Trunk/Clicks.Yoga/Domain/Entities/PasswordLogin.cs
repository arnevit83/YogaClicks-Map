namespace Clicks.Yoga.Domain.Entities
{
    public class PasswordLogin : Entity
    {
        private string _password;

        public static bool PasswordAcceptable(string password)
        {
            return password != null && password.Length > 5;
        }

        public virtual User User { get; set; }

        public string Username { get; set; }

        public string PasswordHash { get; set; }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                HashPassword();
            }
        }

        public bool PasswordCorrect(string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, PasswordHash);
        }

        private void HashPassword()
        {
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(Password, BCrypt.Net.BCrypt.GenerateSalt(12));
        }
    }
}