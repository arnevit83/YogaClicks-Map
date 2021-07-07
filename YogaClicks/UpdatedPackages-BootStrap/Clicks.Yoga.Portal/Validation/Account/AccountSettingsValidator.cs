using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Account;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;

namespace Clicks.Yoga.Portal.Validation.Account
{
    public class AccountSettingsValidator : AbstractValidator<AccountSettingsModel>
    {
        public AccountSettingsValidator()
        {
            RuleFor(e => e.CurrentPassword)
                .NotEmpty().WithLocalizedMessage(() => Shared.PasswordEmpty)
                .When(m => !m.UserHasConnectedFacebook && !string.IsNullOrEmpty(m.NewPassword));

            RuleFor(e => e.NewPassword)
                .Must(PasswordLogin.PasswordAcceptable).WithLocalizedMessage(() => Shared.PasswordUnacceptable)
                .When(m => !string.IsNullOrEmpty(m.NewPassword));

            RuleFor(e => e.NewPasswordConfirmation)
                .NotEmpty().WithLocalizedMessage(() => Shared.PasswordConfirmationEmpty)
                .Equal(e => e.NewPassword).WithLocalizedMessage(() => Shared.PasswordConfirmationDifferent)
                .When(m => !string.IsNullOrEmpty(m.NewPassword));

            RuleFor(e => e.NewEmailAddress)
                .EmailAddress().WithLocalizedMessage(() => Shared.EmailAddressInvalid);

            RuleFor(e => e.NewEmailAddressConfirmation)
                .NotEmpty().WithLocalizedMessage(() => Shared.EmailAddressConfirmationEmpty)
                .Equal(e => e.NewEmailAddress).WithLocalizedMessage(() => Shared.EmailAddressConfirmationDifferent)
                .When(m => !string.IsNullOrEmpty(m.NewEmailAddress));
        }
    }
}