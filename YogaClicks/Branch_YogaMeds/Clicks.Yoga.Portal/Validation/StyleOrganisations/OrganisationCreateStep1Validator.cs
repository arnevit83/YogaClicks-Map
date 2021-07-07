using Clicks.Yoga.Portal.Models.StyleOrganisations;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;
using Local = Clicks.Yoga.Portal.Resources.Errors.StyleOrganisations;

namespace Clicks.Yoga.Portal.Validation.StyleOrganisations
{
    public class OrganisationCreateStep1Validator : AbstractValidator<OrganisationCreateStep1Model>
    {
        public OrganisationCreateStep1Validator()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithLocalizedMessage(() => Shared.NameEmpty)
                .Length(0, 100).WithLocalizedMessage(() => Shared.TextTooLong);

            RuleFor(e => e.Founder)
                .NotEmpty().WithLocalizedMessage(() => Local.FounderEmpty)
                .Length(0, 100).WithLocalizedMessage(() => Shared.TextTooLong);

            RuleFor(e => e.FoundedYear)
                .GreaterThan(0).WithLocalizedMessage(() => Local.FounderEmpty);

            RuleFor(e => e.Style)
                .Must(e => e.Selected).WithLocalizedMessage(() => Local.StyleMissing);
        }
    }
}