using System;
using Clicks.Yoga.Portal.Models.Activities;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;
using Local = Clicks.Yoga.Portal.Resources.Errors.Activities;

namespace Clicks.Yoga.Portal.Validation.Activities
{
    public class ActivityChangeVenueValidator : AbstractValidator<ActivityChangeVenueModel>
    {
        public ActivityChangeVenueValidator()
        {
            RuleFor(m => m.VenueId)
                .NotEmpty()
                .WithLocalizedMessage(() => Local.ChooseVenue);
        }
    }
}