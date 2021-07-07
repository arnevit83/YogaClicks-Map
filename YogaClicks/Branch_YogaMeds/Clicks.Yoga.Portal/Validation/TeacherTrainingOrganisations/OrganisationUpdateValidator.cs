using Clicks.Yoga.Portal.Models.TeacherTrainingOrganisations;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;

namespace Clicks.Yoga.Portal.Validation.TeacherTrainingOrganisations
{
    public class OrganisationUpdateValidator : AbstractValidator<OrganisationUpdateModel>
    {
        public OrganisationUpdateValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithLocalizedMessage(() => Shared.NameEmpty)
                .Length(0, 100).WithLocalizedMessage(() => Shared.TextTooLong);
        }
    }
}