using Clicks.Yoga.Portal.Models.Activities;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;

namespace Clicks.Yoga.Portal.Validation.Activities
{
    public class ActivityUpdateClassValidator : AbstractValidator<ActivityUpdateClassModel>
    {
        public ActivityUpdateClassValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithLocalizedMessage(() => Shared.NameEmpty)
                .Length(0, 100).WithLocalizedMessage(() => Shared.TextTooLong);

            RuleFor(m => m.AbilityLevel)
                .Must(m => m.Selected).WithLocalizedMessage(() => Shared.AbilityLevelMissing);

            RuleFor(m => m.Date)
                .Must(m => m.IsValid).WithLocalizedMessage(() => Shared.StartDateEmpty);

            RuleFor(m => m.TimeRange)
                .Must(m => m.IsValid).WithLocalizedMessage(() => Shared.TimeRangeInvalid);

            RuleFor(m => m.RepeatFrequency)
                .Must(m => m.Selected).When(m => m.Repeat).WithLocalizedMessage(() => Resources.Errors.Activities.RepeatFrequencyMissing);
        }
    }
}