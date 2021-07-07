using Clicks.Yoga.Portal.Models.Account;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;

namespace Clicks.Yoga.Portal.Validation.Account
{
    public class AccountLoginValidator : AbstractValidator<AccountLoginModel>
    {
        public AccountLoginValidator()
        {
            RuleFor(e => e.Username)
                .NotEmpty().WithLocalizedMessage(() => Shared.EmailAddressInvalid);

            RuleFor(e => e.Password)
                .NotEmpty().WithLocalizedMessage(() => Shared.PasswordEmpty);
        }
    }
}