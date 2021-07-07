using Clicks.Yoga.Context;
using Clicks.Yoga.Portal.Models.Profiles;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;

namespace Clicks.Yoga.Portal.Validation.Profiles
{
    public class ProfileCreateTeacherValidator : AbstractValidator<ProfileCreateTeacherModel>
    {
        public ProfileCreateTeacherValidator(ISecurityContext securityContext)
        {
            When(e => !securityContext.CurrentProfile.Professional, () =>
                RuleFor(e => e.Name)
                    .NotEmpty().WithLocalizedMessage(() => Shared.NameEmpty)
                    .Length(0, 100).WithLocalizedMessage(() => Shared.TextTooLong));

            RuleFor(e => e.EmailAddress)
                .NotEmpty().WithLocalizedMessage(() => Shared.EmailAddressInvalid)
                .EmailAddress().WithLocalizedMessage(() => Shared.EmailAddressInvalid);

            //RuleFor(e => e.Telephone)
            //    .NotEmpty().WithLocalizedMessage(() => Shared.TelephoneEmpty)
            //    .Length(0, 100).WithLocalizedMessage(() => Shared.TextTooLong);
        }
    }
}