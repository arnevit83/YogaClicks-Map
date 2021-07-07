using Clicks.Yoga.Portal.Models.Groups;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;

namespace Clicks.Yoga.Portal.Validation.Groups
{
    public class GroupUpdateValidator : AbstractValidator<GroupUpdateModel>
    {
        public GroupUpdateValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithLocalizedMessage(() => Shared.NameEmpty)
                .Length(0, 100).WithLocalizedMessage(() => Shared.TextTooLong);
        }
    }
}