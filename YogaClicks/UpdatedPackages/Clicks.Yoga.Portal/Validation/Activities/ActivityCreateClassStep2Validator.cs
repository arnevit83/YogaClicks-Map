using Clicks.Yoga.Portal.Models.Activities;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;
using Local = Clicks.Yoga.Portal.Resources.Errors.Activities;

namespace Clicks.Yoga.Portal.Validation.Activities
{
    public class ActivityCreateClassStep2Validator : AbstractValidator<ActivityCreateClassStep2Model>
    {
        public ActivityCreateClassStep2Validator()
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

            RuleFor(m => m.RepeatFrequency)
                .Must(m => m.Selected).When(m => m.Repeat).WithLocalizedMessage(() => Local.RepeatFrequencyMissing);
        }
    }
}