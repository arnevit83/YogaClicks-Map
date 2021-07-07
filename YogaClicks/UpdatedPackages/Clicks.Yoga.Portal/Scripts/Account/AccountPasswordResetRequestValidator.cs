using Clicks.Yoga.Portal.Models.Account;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;

namespace Clicks.Yoga.Portal.Validation.Account
{
    public class AccountPasswordResetRequestValidator : AbstractValidator<AccountPasswordResetRequestModel>
    {
        public AccountPasswordResetRequestValidator()
        {
            RuleFor(e => e.Username)
                .NotEmpty().WithLocalizedMessage(() => Shared.EmailAddressInvalid)
                .EmailAddress().WithLocalizedMessage(() => Shared.EmailAddressInvalid);
        }
    }
}