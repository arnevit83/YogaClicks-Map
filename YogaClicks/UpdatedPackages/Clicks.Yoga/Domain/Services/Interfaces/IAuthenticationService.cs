namespace Clicks.Yoga.Domain.Services
{
    public interface IAuthenticationService
    {
        void AuthenticateWithPassword(string username, string password, bool persist);
        void AuthenticateNonActiveuserWithPassword(string username, string password, bool persist);
        void AuthenticateWithFacebook(string accessToken);
        string[] AuthenticateWithFacebookForYogaMap(string accessToken);
        void RequestPasswordReset(string username);
        void ResetPassword(string key, string password);
        void ValidatePasswordResetRequest(string key);
    }
}