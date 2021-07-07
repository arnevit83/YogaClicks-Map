using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public interface IInvitationService
    {
        void InviteFriendFromFacebook(string loginProviderTag, string requestIdentifier, string recipientIdentifier);
        void InviteFriendByEmail(string emailAddress);
        void IntroduceInvitee(FederatedLogin login, out IEnumerable<Invitation> invitation);
        void IntroduceInvitee(Profile invitee, string invitationIdentifier);
    }
}