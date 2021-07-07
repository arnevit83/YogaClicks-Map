using Clicks.Yoga.Portal.Models.Venues;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;

namespace Clicks.Yoga.Portal.Validation.Venues
{
    public class VenueUpdateValidator : AbstractValidator<VenueUpdateModel>
    {
        public VenueUpdateValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithLocalizedMessage(() => Shared.NameEmpty)
                .Length(0, 100).WithLocalizedMessage(() => Shared.TextTooLong);

            RuleFor(e => e.Telephone)
                .NotEmpty().WithLocalizedMessage(() => Shared.TelephoneEmpty)
                .Length(0, 100).WithLocalizedMessage(() => Shared.TextTooLong);

            RuleFor(e => e.EmailAddress)
                .NotEmpty().WithLocalizedMessage(() => Shared.EmailAddressEmpty)
                .EmailAddress().WithLocalizedMessage(() => Shared.EmailAddressInvalid);

            RuleFor(e => e.Address.Line1)
                .NotEmpty().WithLocalizedMessage(() => Shared.AddressFirstLineEmpty)
                .Length(0, 100).WithLocalizedMessage(() => Shared.TextTooLong);

            RuleFor(e => e.Address.City)
                .NotEmpty().WithLocalizedMessage(() => Shared.CityEmpty)
                .Length(0, 100).WithLocalizedMessage(() => Shared.TextTooLong);

            RuleFor(e => e.Address.Postcode)
                .NotEmpty().WithLocalizedMessage(() => Shared.PostcodeEmpty)
                .Length(0, 20).WithLocalizedMessage(() => Shared.TextTooLong);

            RuleFor(e => e.Address.Country.Selection)
                .NotNull().WithLocalizedMessage(() => Shared.CountryNull);
        }
    }
}