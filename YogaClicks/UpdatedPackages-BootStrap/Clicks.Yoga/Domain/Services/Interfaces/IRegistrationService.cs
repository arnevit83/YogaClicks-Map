using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public interface IRegistrationService
    {
        void RegisterAccount(Profile profile, User user, PasswordLogin login);
        User ActivateAccount(string key);
    }
}