using Clicks.Yoga.Portal.Models.SignUp;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;

namespace Clicks.Yoga.Portal.Validation.SignUp
{
    public class ProfileStep2ViewModelValidator : AbstractValidator<ProfileStep2ViewModel>
    {
        public ProfileStep2ViewModelValidator()
        {
            RuleFor(e => e.Forename)
                .NotEmpty().WithLocalizedMessage(() => Shared.ForenameEmpty)
                .Length(1, 100).WithLocalizedMessage(() => Shared.TextTooLong);

            RuleFor(e => e.Surname)
                .NotEmpty().WithLocalizedMessage(() => Shared.SurnameEmpty)
                .Length(1, 100).WithLocalizedMessage(() => Shared.TextTooLong);

            RuleFor(e => e.EmailAddress)
                .NotEmpty().WithLocalizedMessage(() => Shared.EmailAddressInvalid)
                .Length(1, 100).WithLocalizedMessage(() => Shared.TextTooLong);

            RuleFor(e => e.Password)
                .NotEmpty().WithLocalizedMessage(() => Shared.PasswordInvalid)
                .Length(6, 100).WithLocalizedMessage(() => Shared.TextTooLong);
        }
    }
}