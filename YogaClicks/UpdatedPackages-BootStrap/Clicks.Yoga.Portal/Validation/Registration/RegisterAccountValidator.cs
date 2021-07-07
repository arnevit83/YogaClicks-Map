using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Registration;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;

namespace Clicks.Yoga.Portal.Validation.Registration
{
    public class RegisterAccountValidator : AbstractValidator<RegisterAccountModel>
    {
        public RegisterAccountValidator()
        {
            When(e => !e.Professional, () =>
            {
                RuleFor(e => e.Forename)
                    .NotEmpty().WithLocalizedMessage(() => Shared.ForenameEmpty)
                    .Length(0, 100).WithLocalizedMessage(() => Shared.TextTooLong);

                RuleFor(e => e.Surname)
                    .NotEmpty().WithLocalizedMessage(() => Shared.SurnameEmpty)
                    .Length(0, 100).WithLocalizedMessage(() => Shared.TextTooLong);
            });

            RuleFor(e => e.Username)
                .NotEmpty().WithLocalizedMessage(() => Shared.EmailAddressInvalid)
                .EmailAddress().WithLocalizedMessage(() => Shared.EmailAddressInvalid);

            RuleFor(e => e.Password)
                .NotEmpty().WithLocalizedMessage(() => Shared.PasswordEmpty)
                .Must(PasswordLogin.PasswordAcceptable).WithLocalizedMessage(() => Shared.PasswordInvalid);

            RuleFor(e => e.Password)
                .Equal(e => e.PasswordConfirmation).WithLocalizedMessage(() => Shared.PasswordConfirmationDifferent);
        }
    }
}