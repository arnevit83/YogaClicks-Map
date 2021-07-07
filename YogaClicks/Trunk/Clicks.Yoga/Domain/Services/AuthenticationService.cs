using System;
using System.Collections.Generic;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Common.ExceptionHandling;
using Facebook;

namespace Clicks.Yoga.Domain.Services
{
    public class AuthenticationService : ServiceBase, IAuthenticationService
    {
        public AuthenticationService(
            IEntityContext entityContext,
            ISecurityContext securityContext,
            IAccountService accountService,
            IInvitationService invitationService)
            : base(entityContext, securityContext)
        {
            InvitationService = invitationService;
            AccountService = accountService;
        }

        private IInvitationService InvitationService { get; set; }
        private IAccountService AccountService { get; set; }

        public void AuthenticateWithPassword(string username, string password, bool persist)
        {
            var login = EntityContext.PasswordLogins.Get(e => e.Username == username && e.User.Active);

            if (login != null && login.PasswordCorrect(password))
            {
                SecurityContext.SetCurrentUser(login.User, persist);
            }
            else
            {
                throw new AuthenticationException("Invalid username or password.");
            }
        }

        public void AuthenticateWithFacebook(string accessToken)
        {
            dynamic details = null;
            string userId = null;

            var client = new FacebookClient(accessToken);

            try
            {
                details = client.Get("me", new { fields = "id,email,first_name,last_name,gender" });
                userId = details.id;
            }
            catch (Exception ex)
            {
                throw new AuthenticationProviderException(ex.Message, ex);
            }

            if (string.IsNullOrEmpty(userId))
            {
                throw new AuthenticationProviderException("Facebook returned an invalid user id.");
            }

            var user = SecurityContext.CurrentUser;
            var login = EntityContext.FederatedLogins.Get(e => e.Provider.IsFacebook && e.Identifier == userId && e.User.Active);

            Profile profile = null;

            if (user == null && login != null)
            {
                user = login.User;
            }

            if (user == null)
            {
                string forename = details.first_name;
                string surname = details.last_name;
                string email = details.email;
                string gender = details.gender;

                user = EntityContext.Users.Get(e => e.EmailAddress == email && e.Active);

                if (user == null)
                {
                    user = new User
                    {
                        EmailAddress = email,
                        Active = true
                    };

                    profile = new Profile
                    {
                        Owner = user,
                        Forename = forename,
                        Surname = surname,
                        EmailAddress = email,
                        Gender = EntityContext.Genders.Get(e => e.Tag.ToLower() == gender),
                        Wall = new ProfileWall()
                    };

                    EntityContext.Profiles.Add(profile);
                    AccountService.AutoJoinGroups(user);
                }
            }

            if (login == null)
            {
                login = new FederatedLogin
                {
                    User = user,
                    Provider = EntityContext.FederatedLoginProviders.Get(e => e.IsFacebook),
                    Identifier = userId
                };

                EntityContext.FederatedLogins.Add(login);
            }
            else if (user.Id != login.User.Id)
            {
                throw new DuplicateUsernameException();
            }

            if (user.IsTransient) EntityContext.Users.Add(user);
            if (profile != null && profile.IsTransient) EntityContext.Profiles.Add(profile);

            if (login.IsTransient)
            {
                IEnumerable<Invitation> invitations = null;

                EntityContext.FederatedLogins.Add(login);
                InvitationService.IntroduceInvitee(login, out invitations);

                foreach (var invitation in invitations)
                {
                    try
                    {
                        client.Delete(string.Format("/{0}", invitation.RequestIdentifier));
                    }
                    catch (Exception ex)
                    {
                        ExceptionHandler.Handle(ex);
                    }
                }
            }

            SecurityContext.SetCurrentUser(user, false);
        }

        public void RequestPasswordReset(string username)
        {
            var login = EntityContext.PasswordLogins.Get(e => e.Username == username);

            if (login == null)
                throw new UnknownIdentifierException();

            if (!login.User.Active)
            {
                var activation = EntityContext.UserActivationRequests.Get(e => !e.Completed && e.User.EmailAddress == username);

                if (activation != null)
                {
                    activation.ActivateUser();
                }
            }

            if (!login.User.Active)
                throw new UnknownIdentifierException();

            var request = EntityContext.PasswordResetRequests.Get(e => e.Login.Id == login.Id && !e.Completed && e.ExpiryTime > DateTime.UtcNow);

            if (request == null)
            {
                request = new PasswordResetRequest();

                request.Login = login;
                request.ClientAddress = SecurityContext.ClientAddress;

                EntityContext.PasswordResetRequests.Add(request);
            }

            request.SendEmail();
        }

        public void ResetPassword(string key, string password)
        {
            var request = EntityContext.PasswordResetRequests.Get(e => e.Key == key);
            
            ValidatePasswordResetRequest(request);

            request.Login.Password = password;
            request.Completed = true;

            var action = new PasswordResetAction();

            action.Request = request;
            action.ClientAddress = SecurityContext.ClientAddress;

            EntityContext.PasswordResetActions.Add(action);
        }

        public void ValidatePasswordResetRequest(string key)
        {
            var request = EntityContext.PasswordResetRequests.Get(e => e.Key == key);
            ValidatePasswordResetRequest(request);
        }

        private void ValidatePasswordResetRequest(PasswordResetRequest request)
        {
            if (request == null)
                throw new UnknownIdentifierException();

            if (!request.Login.User.Active)
                throw new UnknownEntityException();

            if (request.Completed)
                throw new RepeatedActionException();

            if (request.Expired)
                throw new OpportunityExpiredException();
        }
    }
}