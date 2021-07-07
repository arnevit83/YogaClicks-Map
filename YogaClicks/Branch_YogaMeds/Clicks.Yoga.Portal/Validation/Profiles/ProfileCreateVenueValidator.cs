using Clicks.Yoga.Portal.Models.Profiles;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;

namespace Clicks.Yoga.Portal.Validation.Profiles
{
    public class ProfileCreateVenueValidator : AbstractValidator<ProfileCreateVenueModel>
    {
        public ProfileCreateVenueValidator()
        {
            RuleFor(e => e.Venue.Name)
                .NotEmpty().WithLocalizedMessage(() => Shared.NameEmpty)
                .Length(0, 100).WithLocalizedMessage(() => Shared.TextTooLong);
        }
    }
}