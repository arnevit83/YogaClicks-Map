using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public interface IAccountService
    {
        bool UsernameExists(string username);
        bool PasswordCorrect(User user, string password);
        bool CurrentUserHasPasswordLogin();
        bool CurrentUserHasFederatedLogin(string providerTag);
        User GetUser(int id);
        void AddUser(User user);
        void AddPasswordLogin(PasswordLogin login);
        void RequestUserActivation(User user);
        User ActivateUser(string requestKey);
        void AutoJoinGroups(User user);
        void ChangePassword(User user, string currentPassword, string password);
        void SetPassword(User user, string password);
        void RequestEmailAddressChange(User user, string address);
        void ConfirmEmailAddressChange(string requestKey);
        void ChangeEmailAddress(User user, string address);
        bool ResendUserActivation(string emailAddress);
        void RemoveFederatedLogin(User user, string providerTag);
        void ChangeActor(int id, string typeName);
        void Deactivate(User user, DeactivationStrategy strategy);
    }
}