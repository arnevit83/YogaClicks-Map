using Clicks.Yoga.Portal.Models.Profiles;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;

namespace Clicks.Yoga.Portal.Validation.Profiles
{
    public class ProfileCreateVenueContactValidator : AbstractValidator<ProfileCreateVenueContactModel>
    {
        public ProfileCreateVenueContactValidator()
        {
            RuleFor(e => e.Telephone)
                .NotEmpty().WithLocalizedMessage(() => Shared.TelephoneEmpty)
                .Length(0, 100).WithLocalizedMessage(() => Shared.TextTooLong);

            RuleFor(e => e.EmailAddress)
                .NotEmpty().WithLocalizedMessage(() => Shared.EmailAddressInvalid)
                .EmailAddress().WithLocalizedMessage(() => Shared.EmailAddressInvalid);
        }
    }
}