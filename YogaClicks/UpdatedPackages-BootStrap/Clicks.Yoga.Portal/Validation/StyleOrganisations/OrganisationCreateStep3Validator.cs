using Clicks.Yoga.Portal.Models.StyleOrganisations;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;

namespace Clicks.Yoga.Portal.Validation.StyleOrganisations
{
    public class OrganisationCreateStep3Validator : AbstractValidator<OrganisationCreateStep3Model>
    {
        public OrganisationCreateStep3Validator()
        {
            RuleFor(e => e.Description)
                .NotEmpty().WithLocalizedMessage(() => Shared.DescriptionEmpty)
                .Length(0, 1200).WithLocalizedMessage(() => Shared.TextTooLong);
        }
    }
}