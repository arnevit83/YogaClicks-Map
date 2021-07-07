using System;
using System.Linq;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Emails;
using Clicks.Yoga.MailingLists;

namespace Clicks.Yoga.Domain.Services
{
    public class AccountService : ServiceBase, IAccountService
    {
        public AccountService(
            IEntityContext entityContext,
            ISecurityContext securityContext,
            IEntityService entityService,
            IProfileService profileService,
            IGroupService groupService,
            IMailingListProvider mailingListProvider,
            INotificationService notificationService)
            : base(entityContext, securityContext)
        {
            EntityService = entityService;
            MailingListProvider = mailingListProvider;
            ProfileService = profileService;
            GroupService = groupService;
            NotificationService = notificationService;
        }

        private IEntityService EntityService { get; set; }

        private IMailingListProvider MailingListProvider { get; set; }

        private IProfileService ProfileService { get; set; }

        private IGroupService GroupService { get; set; }
        
        private INotificationService NotificationService { get; set; }

        public bool UsernameExists(string username)
        {
            return EntityContext.PasswordLogins.Any(e => e.Username == username && e.User.Active);
        }

        public bool UsernameExists(string username, int userId)
        {
            return EntityContext.PasswordLogins.Any(e => e.Username == username && e.User.Active && e.User.Id != userId);
        }

        public bool PasswordCorrect(User user, string password)
        {
            var login = EntityContext.PasswordLogins.Get(e => e.User.Id == user.Id);

            if (login == null) throw new InvalidOperationException("User does not have a password login.");

            return login.PasswordCorrect(password);
        }

        public bool CurrentUserHasPasswordLogin()
        {
            if (!SecurityContext.Authenticated) return false;
            return EntityContext.PasswordLogins.Any(e => e.User.Id == SecurityContext.CurrentUser.Id);
        }

        public bool CurrentUserHasFederatedLogin(string providerTag)
        {
            return EntityContext.FederatedLogins.Any(e => e.User.Id == SecurityContext.CurrentUser.Id && e.Provider.Tag == providerTag);
        }

        public User GetUser(int id)
        {
            return EntityContext.Users.Get(id);
        }

        public void AddUser(User user)
        {
            EntityContext.Users.Add(user);
        }

        public void AddPasswordLogin(PasswordLogin login)
        {
            if (UsernameExists(login.Username))
                throw new DuplicateUsernameException();

            EntityContext.PasswordLogins.Add(login);
        }

        public void RequestUserActivation(User user)
        {
            var request = new UserActivationRequest();

            request.User = user;

            EntityContext.UserActivationRequests.Add(request);

            new UserActivationRequestEmail { Request = request }.Send();
        }

        public User ActivateUser(string requestKey)
        {
            var request = EntityContext.UserActivationRequests.Get(e => e.Key == requestKey);

            if (request == null)
                throw new UnknownEntityException();
            if (request.Completed)
                throw new RepeatedActionException();
            if (UsernameExists(request.User.EmailAddress))
                throw new DuplicateUsernameException();

            request.ActivateUser();

            SecurityContext.SetCurrentUser(request.User, false);

            ClaimEntityInvites(request.User);

            AutoJoinGroups(request.User);

            SubscribeToMailingLists(request.User);
            EnableNotificationsForUser(request.User);

            return request.User;
        }

        private void SubscribeToMailingLists(User user)
        {
            var profile = ProfileService.GetProfile(user.Id);

            MailingListProvider.Unsubscribe(MailingList.Teachers, user.EmailAddress);
            MailingListProvider.Unsubscribe(MailingList.Venues, user.EmailAddress);

            MailingListProvider.Subsribe(MailingList.Service, user.EmailAddress, profile.Forename, profile.Surname);
            MailingListProvider.Subsribe(MailingList.Students, user.EmailAddress, profile.Forename, profile.Surname);
        }

        private void EnableNotificationsForUser(User user)
        {
            NotificationService.ToggleNotificationDigest(user, true, "Month");
        }


        public void AutoJoinGroups(User user)
        {
            var groups = EntityContext.Groups.Where(e => e.Active && e.AutoJoin).ToList();

            foreach (var group in groups)
            {
                var member = new GroupMember(group, user, false, true);

                EntityContext.GroupMembers.Add(member);
            }
        }

        public void ChangePassword(User user, string currentPassword, string newPassword)
        {
            if (!PasswordLogin.PasswordAcceptable(newPassword))
                throw new UnacceptablePasswordException();

            var login = EntityContext.PasswordLogins.Get(e => e.User.Id == user.Id);

            if (!login.PasswordCorrect(currentPassword))
                throw new AuthenticationException("The supplied current password is incorrect.");

            login.Password = newPassword;
        }

        public void SetPassword(User user, string password)
        {
            var login = EntityContext.PasswordLogins.Get(e => e.User.Id == user.Id);

            login.Password = password;
        }

        public void RequestEmailAddressChange(User user, string address)
        {
            if (user == null)
                throw new ArgumentNullException("user");
            if (address == null)
                throw new ArgumentNullException("address");
            if (UsernameExists(address))
                throw new DuplicateUsernameException();

            var request = new UserEmailAddressChangeRequest(user, address, SecurityContext);
            var email = new UserEmailAddressChangeRequestEmail { Request = request };

            EntityContext.UserEmailAddressChangeRequests.Add(request);
            email.Send();
        }

        public void ConfirmEmailAddressChange(string requestKey)
        {
            var request = EntityContext.UserEmailAddressChangeRequests.Get(e => e.Key == requestKey);

            if (request == null)
                throw new UnknownIdentifierException();
            if (request.Completed)
                throw new RepeatedActionException();
            if (request.Expired)
                throw new OpportunityExpiredException();
            if (UsernameExists(request.EmailAddress))
                throw new DuplicateUsernameException();

            var old = request.User.EmailAddress;

            request.Complete(SecurityContext);

            MailingListProvider.ChangeEmailAddress(old, request.EmailAddress);
        }

        public void ChangeEmailAddress(User user, string address)
        {
            if (UsernameExists(address))
                throw new DuplicateUsernameException();
            if (!SecurityContext.IsSuperUser())
                throw new PermissionDeniedException();

            var login = EntityContext.PasswordLogins.Get(e => e.User.Id == user.Id);

            var oldAddress = user.EmailAddress;
            user.EmailAddress = address;
            if (login != null) login.Username = address;

            MailingListProvider.ChangeEmailAddress(oldAddress, address);
        }

        public bool ResendUserActivation(string emailAddress)
        {
            var request = EntityContext.UserActivationRequests.Get(e => !e.Completed && e.User.EmailAddress == emailAddress);

            if (request != null)
            {
                new UserActivationRequestEmail { Request = request }.Send();

                return true;
            }

            return false;
        }

        public void RemoveFederatedLogin(User user, string providerTag)
        {
            var login = EntityContext.FederatedLogins.Get(e => e.User.Id == user.Id && e.Provider.Tag == providerTag);

            if (login == null) return;

            EntityContext.FederatedLogins.Remove(login);
        }

        public void ChangeActor(int id, string typeName)
        {
            if (!SecurityContext.Authenticated)
                throw new PermissionDeniedException();
            if (typeName == null)
                throw new ArgumentNullException("typeName");

            var actor = EntityService.GetEntity<IActor>(id, typeName);

            if (actor == null)
                throw new ArgumentException("Actor does not exist or does not implement IActor.");

            if (!SecurityContext.IsOwner(actor))
                throw new PermissionDeniedException();

            SecurityContext.SetCurrentActor(actor);
        }

        public void Deactivate(User user, DeactivationStrategy strategy)
        {
            user.Active = false;
            user.Profile.Active = false;

            foreach (var fanned in EntityContext.Fans.Where(e => e.User.Id == user.Id).ToList())
            {
                EntityContext.Fans.Remove(fanned);
            }

            foreach (var friendship in user.Profile.Friendships.ToList())
            {
                friendship.Confirmed = false;
                friendship.Inverse.Confirmed = false;
            }

            foreach (var group in GroupService.GetJoinedGroups(user.Id))
            {
                var member = group.Members.First(e => e.User.Id == user.Id);

                EntityContext.GroupMembers.Remove(member);
            }

            foreach (var comment in EntityContext.Comments.Where(e => e.Owner != null && e.Owner.Id == user.Id).ToList())
            {
                EntityContext.Comments.Remove(comment);
            }

            foreach (var review in EntityContext.Reviews.Where(e => e.Owner.Id == user.Id).ToList())
            {
                EntityContext.Reviews.Remove(review);
            }

            foreach (var venue in EntityContext.Venues.Where(e => e.Owner.Id == user.Id).ToList())
            {
                Detach(venue, strategy);
            }

            foreach (var teacher in EntityContext.Teachers.Where(e => e.Owner.Id == user.Id).ToList())
            {
                Detach(teacher, strategy);
            }

            foreach (var org in EntityContext.StyleOrganisations.Where(e => e.Owner.Id == user.Id).ToList())
            {
                Detach(org, strategy);
            }

            foreach (var org in EntityContext.TeacherTrainingOrganisations.Where(e => e.Owner.Id == user.Id).ToList())
            {
                if (strategy == DeactivationStrategy.Delete)
                {
                    foreach (var course in org.Courses.ToList())
                    {
                        course.Active = false;
                    }
                }

                Detach(org, strategy);
            }

            foreach (var activity in EntityContext.Activities.Where(e => e.Owner.Id == user.Id).ToList())
            {
                Detach(activity, strategy);
            }

            foreach (var group in EntityContext.Groups.Where(e => e.Owner.Id == user.Id).ToList())
            {
                var member = group.Members.FirstOrDefault(e => e.User.Id == user.Id);

                if (member != null)
                {
                    EntityContext.GroupMembers.Remove(member);
                }

                Detach(group, strategy);
            }

            foreach (var login in EntityContext.PasswordLogins.Where(e => e.User.Id == user.Id).ToList())
            {
                login.Username = "";
                login.Password = "";
            }
        }

        private void Detach(PrincipalEntity entity, DeactivationStrategy strategy)
        {
            if (strategy == DeactivationStrategy.Delete)
            {
                entity.Active = false;
            }
            else
            {
                entity.Owner = null;
            }
        }

        private void ClaimEntityInvites(User user)
        {
            var email = user.EmailAddress.ToLower();
            var invites = EntityContext.EntityHandleInvites.Where(e => e.EmailAddress.ToLower() == email).ToList();

            for (var i = invites.Count - 1; i >= 0; i--)
            {
                var invite = invites[i];

                if (invite.EntityHandle.Owner == null)
                {
                    ProfileService.Claim(invite.EntityHandle.EntityId, invite.EntityHandle.EntityType.Name);
                }

                EntityContext.EntityHandleInvites.Remove(invite);
            }
        }

   
    }
}