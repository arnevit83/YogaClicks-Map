using System.Linq;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.MailingLists;

namespace Clicks.Yoga.Domain.Services
{
    public class RegistrationService : ServiceBase, IRegistrationService
    {
        public RegistrationService(
            IEntityContext entityContext,
            ISecurityContext securityContext,
            IMailingListProvider mailingListProvider,
            IAccountService accountService,
            IProfileService profileService)
            : base(entityContext, securityContext)
        {
            MailingListProvider = mailingListProvider;
            AccountService = accountService;
            ProfileService = profileService;
        }

        private IMailingListProvider MailingListProvider { get; set; }

        private IAccountService AccountService { get; set; }

        private IProfileService ProfileService { get; set; }

        public void RegisterAccount(Profile profile, User user, PasswordLogin login)
        {
            if (SecurityContext.Authenticated)
                throw new AuthenticatedException();

            login.User = user;
            profile.Owner = user;

            if (AccountService.UsernameExists(login.Username))
                throw new DuplicateUsernameException();

            if (EntityContext.UserActivationRequests.Any(r => r.User.EmailAddress == user.EmailAddress && !r.Completed))
            {
                throw new DuplicateUsernameException();
            }

            user.Active = false;
            profile.Active = false;

            profile.Wall = new ProfileWall();

            AccountService.AddUser(user);
            AccountService.AddPasswordLogin(login);
            ProfileService.AddProfile(profile);
            AccountService.RequestUserActivation(user);
        }

        public User ActivateAccount(string key)
        {
            var user = AccountService.ActivateUser(key);

            return user;
        }
    }
}
