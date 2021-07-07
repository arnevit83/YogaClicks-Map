using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Account;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;

namespace Clicks.Yoga.Portal.Validation.Account
{
    public class AccountPasswordResetValidator : AbstractValidator<AccountPasswordResetModel>
    {
        public AccountPasswordResetValidator()
        {
            RuleFor(e => e.Password)
                .NotEmpty().WithLocalizedMessage(() => Shared.PasswordEmpty)
                .Must(v => PasswordLogin.PasswordAcceptable(v)).WithLocalizedMessage(() => Shared.PasswordInvalid);

            RuleFor(e => e.PasswordConfirmation)
                .Equal(e => e.Password).WithLocalizedMessage(() => Shared.PasswordConfirmationDifferent);
        }
    }
}