using Clicks.Yoga.Portal.Models.Sharing;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;

namespace Clicks.Yoga.Portal.Validation.Sharing
{
    public class InviteFriendsValidator : AbstractValidator<SharingEntityByEmailModel>
    {
        public InviteFriendsValidator()
        {
            RuleFor(e => e.EmailAddress)
                .NotEmpty().WithLocalizedMessage(() => Shared.EmailAddressInvalid)
                .EmailAddress().WithLocalizedMessage(() => Shared.EmailAddressInvalid);

            RuleFor(e => e.Message)
                .Length(0, 400).WithLocalizedMessage(() => Shared.TextTooLong);
        }
    }
}