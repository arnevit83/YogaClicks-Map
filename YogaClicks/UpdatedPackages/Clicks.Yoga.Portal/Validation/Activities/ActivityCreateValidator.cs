using Clicks.Yoga.Portal.Models.Activities;
using Clicks.Yoga.Portal.Models.SignUp;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;
using Local = Clicks.Yoga.Portal.Resources.Errors.Activities;

namespace Clicks.Yoga.Portal.Validation.Activities
{
    public class ActivityCreateValidator : AbstractValidator<ActivityCreateViewModel>
    {
        public ActivityCreateValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithLocalizedMessage(() => Shared.NameEmpty)
                .Length(0, 50).WithLocalizedMessage(() => Shared.TextTooLong);

            RuleFor(m => m.AbilityLevel)
                .Must(m => m.Selected).WithLocalizedMessage(() => Shared.AbilityLevelMissing);

            RuleFor(m => m.Date)
                .Must(m => m.IsValid).WithLocalizedMessage(() => Shared.StartDateEmpty);

            RuleFor(m => m.TimeRange)
                .Must(m => m.IsValid).WithLocalizedMessage(() => Shared.TimeRangeInvalid);
        }
    }
}