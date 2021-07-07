using Clicks.Yoga.Portal.Models.Venues;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;

namespace Clicks.Yoga.Portal.Validation.Venues
{
    public class VenueClaimStep3Validator : AbstractValidator<VenueClaimStep3Model>
    {
        public VenueClaimStep3Validator()
        {
            RuleFor(e => e.Address.Line1)
                .NotEmpty().WithLocalizedMessage(() => Shared.AddressEmpty)
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