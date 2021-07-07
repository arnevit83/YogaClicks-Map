using Clicks.Yoga.Portal.Models.Venues;
using FluentValidation;
using Local = Clicks.Yoga.Portal.Resources.Errors.Venues;

namespace Clicks.Yoga.Portal.Validation.Venues
{
    public class VenueClaimStep1Validator : AbstractValidator<VenueClaimStep1Model>
    {
        public VenueClaimStep1Validator()
        {
            RuleFor(m => m.Owned)
                .Equal(true)
                .WithLocalizedMessage(() => Local.ConfirmOwnership);
        }
    }
}