using Clicks.Yoga.Portal.Models.Venues;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;
using Local = Clicks.Yoga.Portal.Resources.Errors.Venues;

namespace Clicks.Yoga.Portal.Validation.Venues
{
    public class VenueClaimStep2Validator : AbstractValidator<VenueClaimStep2Model>
    {
        public VenueClaimStep2Validator()
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