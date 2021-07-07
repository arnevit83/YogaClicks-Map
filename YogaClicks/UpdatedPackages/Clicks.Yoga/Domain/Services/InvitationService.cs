using System;
using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Emails;

namespace Clicks.Yoga.Domain.Services
{
    public class InvitationService : ServiceBase, IInvitationService
    {
        public InvitationService(
            IEntityContext entityContext,
            ISecurityContext securityContext,
            IFriendService friendService)
            : base(entityContext, securityContext)
        {
            FriendService = friendService;
        }

        private IFriendService FriendService { get; set; }

        public void InviteFriendFromFacebook(string loginProviderTag, string requestIdentifier, string recipientIdentifier)
        {
            if (loginProviderTag == null)
                throw new ArgumentNullException("loginProviderTag");
            if (recipientIdentifier == null)
                throw new ArgumentNullException("recipientIdentifier");

            var loginProvider = EntityContext.FederatedLoginProviders.Get(e => e.Tag == loginProviderTag);

            if (loginProvider == null)
                throw new ArgumentOutOfRangeException("loginProviderTag");

            var invitation = new Invitation
            {
                Sender = SecurityContext.CurrentUser.Profile,
                LoginProvider = loginProvider,
                RequestIdentifier = requestIdentifier,
                RecipientIdentifier = recipientIdentifier
            };

            EntityContext.Invitations.Add(invitation);
        }

        public void InviteFriendByEmail(string emailAddress)
        {
            var invitation = new Invitation
            {
                Sender = SecurityContext.CurrentUser.Profile,
                RequestIdentifier = Guid.NewGuid().ToString(),
                RecipientIdentifier = emailAddress
            };

            EntityContext.Invitations.Add(invitation);

            new InviteFriendEmail(invitation).Send();
        }

        public void IntroduceInvitee(FederatedLogin login, out IEnumerable<Invitation> invitations)
        {
            if (login == null)
                throw new ArgumentNullException("login");

            invitations = EntityContext.Invitations
                .Where(e => e.LoginProvider.Id == login.Provider.Id)
                .Where(e => e.RecipientIdentifier == login.Identifier)
                .ToList();

            foreach (var invitation in invitations)
            {
                FriendService.Befriend(invitation.Sender, login.User.Profile);
            }
        }

        public void IntroduceInvitee(Profile invitee, string invitationIdentifier)
        {
            if (invitee == null)
                throw new ArgumentNullException("invitee");

            var invitations = EntityContext.Invitations.Where(e =>
                e.RecipientIdentifier == invitee.Owner.EmailAddress ||
                e.RequestIdentifier == invitationIdentifier)
                .ToList();

            foreach (var invitation in invitations)
            {
                FriendService.Befriend(invitation.Sender, invitee);
            }
        }
    }
}