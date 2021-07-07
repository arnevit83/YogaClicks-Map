using System.Linq;
using Clicks.Yoga.Portal.Models.Activities;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;
using Local = Clicks.Yoga.Portal.Resources.Errors.Activities;

namespace Clicks.Yoga.Portal.Validation.Activities
{
    public class ActivityCreateClassStep5Validator : AbstractValidator<ActivityCreateClassStep5Model>
    {
        public ActivityCreateClassStep5Validator()
        {
            RuleFor(e => e.VenueId)
                .NotEmpty()
                .WithLocalizedMessage(() => Local.ChooseVenue);
        }
    }
}