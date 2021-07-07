using System.Linq;
using Clicks.Yoga.Portal.Models.Activities;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;
using Local = Clicks.Yoga.Portal.Resources.Errors.Activities;

namespace Clicks.Yoga.Portal.Validation.Activities
{
    public class ActivityCreateStep4Validator : AbstractValidator<ActivityCreateStep4Model>
    {
        public ActivityCreateStep4Validator()
        {
            RuleFor(e => e.VenueId)
                .NotEmpty()
                .WithLocalizedMessage(() => Local.ChooseVenue);
        }
    }
}