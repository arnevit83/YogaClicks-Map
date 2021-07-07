using Clicks.Yoga.Portal.Models.Venues;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;

namespace Clicks.Yoga.Portal.Validation.Venues
{
    public class VenueCreatePlacementValidator : AbstractValidator<VenueCreatePlacementModel>
    {
        public VenueCreatePlacementValidator()
        {
            RuleFor(m => m.Teacher.Id)
                .NotNull()
                .WithLocalizedMessage(() => Shared.TeacherMissing);
        }
    }
}