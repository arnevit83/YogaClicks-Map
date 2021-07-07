using Clicks.Yoga.Portal.Models.Venues;
using FluentValidation;
using Local = Clicks.Yoga.Portal.Resources.Errors.Venues;

namespace Clicks.Yoga.Portal.Validation.Venues
{
    public class VenueObtainStep1Validator : AbstractValidator<VenueObtainStep1Model>
    {
        public VenueObtainStep1Validator()
        {
            RuleFor(m => m.Venue.Name)
                .NotEmpty().When(m => m.Venue.Id == null).WithLocalizedMessage(() => Local.VenueNameAndIdEmpty);
        }
    }
}