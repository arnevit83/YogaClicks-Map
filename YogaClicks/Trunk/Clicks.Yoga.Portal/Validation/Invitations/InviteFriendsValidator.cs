using Clicks.Yoga.Portal.Models.Invitations;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;

namespace Clicks.Yoga.Portal.Validation.Invitations
{
    public class InviteFriendsValidator : AbstractValidator<InvitationInviteFriendsByEmailModel>
    {
        public InviteFriendsValidator()
        {
            RuleFor(e => e.EmailAddress)
                .NotEmpty().WithLocalizedMessage(() => Shared.EmailAddressInvalid)
                .EmailAddress().WithLocalizedMessage(() => Shared.EmailAddressInvalid);
        }
    }
}