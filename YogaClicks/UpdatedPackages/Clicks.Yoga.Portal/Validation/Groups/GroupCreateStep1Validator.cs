using Clicks.Yoga.Portal.Models.Groups;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;

namespace Clicks.Yoga.Portal.Validation.Groups
{
    public class GroupCreateStep1Validator : AbstractValidator<GroupCreateStep1Model>
    {
        public GroupCreateStep1Validator()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithLocalizedMessage(() => Shared.NameEmpty)
                .Length(0, 100).WithLocalizedMessage(() => Shared.TextTooLong);
        }
    }
}