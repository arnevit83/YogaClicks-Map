using System;
using Clicks.Yoga.Portal.Models.Activities;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;

namespace Clicks.Yoga.Portal.Validation.Activities
{
    public class ActivityCreateStep2Validator : AbstractValidator<ActivityCreateStep2Model>
    {
        public ActivityCreateStep2Validator()
        {
            RuleFor(m => m.StartTime)
                .Must(m => m.IsValid).WithLocalizedMessage(() => Shared.StartDateEmpty);

            RuleFor(m => m.FinishTime)
                .Must(m => m.IsValid).WithLocalizedMessage(() => Shared.FinishDateEmpty);

            RuleFor(m => m.StartTime.DateTime.Value)
                .GreaterThan(DateTime.Now)
                .When(m => m.StartTime.IsValid)
                .WithLocalizedMessage(() => Shared.StartDateInFuture);

            RuleFor(m => m.FinishTime.DateTime.Value)
                .GreaterThan(m => m.StartTime.DateTime.Value)
                .When(m => m.FinishTime.IsValid && m.StartTime.IsValid)
                .WithLocalizedMessage(() => Shared.StartDateAfterFinishDate);

            RuleFor(m => m.Description)
                .NotEmpty().WithLocalizedMessage(() => Shared.DescriptionEmpty);
        }
    }
}