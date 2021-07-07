using Clicks.Yoga.Portal.Models.Activities;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;

namespace Clicks.Yoga.Portal.Validation.Activities
{
    public class ActivityCreateStep1Validator : AbstractValidator<ActivityCreateStep1Model>
    {
        public ActivityCreateStep1Validator()
        {
            RuleFor(m => m.Type)
                .Must(e => e.Selected).WithLocalizedMessage(() => Shared.TypeMissing);

            When(m => m.Type.Id > 1, () => RuleFor(m => m.Name)
                .NotEmpty().WithLocalizedMessage(() => Shared.NameEmpty)
                .Length(0, 50).WithLocalizedMessage(() => Shared.TextTooLong));
        }
    }
}