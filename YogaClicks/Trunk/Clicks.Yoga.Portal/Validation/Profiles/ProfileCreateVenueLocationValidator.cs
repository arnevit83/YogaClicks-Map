using Clicks.Yoga.Portal.Models.Profiles;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;

namespace Clicks.Yoga.Portal.Validation.Profiles
{
    public class ProfileCreateVenueLocationValidator : AbstractValidator<ProfileCreateVenueLocationModel>
    {
        public ProfileCreateVenueLocationValidator()
        {
            RuleFor(e => e.Address.Line1)
                .NotEmpty().WithLocalizedMessage(() => Shared.AddressFirstLineEmpty)
                .Length(0, 100).WithLocalizedMessage(() => Shared.TextTooLong);

            RuleFor(e => e.Address.City)
                .NotEmpty().WithLocalizedMessage(() => Shared.CityEmpty)
                .Length(0, 100).WithLocalizedMessage(() => Shared.TextTooLong);

            RuleFor(e => e.Address.Postcode)
                .NotEmpty().WithLocalizedMessage(() => Shared.PostcodeEmpty)
                .Length(0, 100).WithLocalizedMessage(() => Shared.TextTooLong);

            RuleFor(e => e.Address.Country.Selection)
                .NotNull().WithLocalizedMessage(() => Shared.CountryNull);
        }
    }
}