using Clicks.Yoga.Portal.Models.StyleOrganisations;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;
using Local = Clicks.Yoga.Portal.Resources.Errors.StyleOrganisations;

namespace Clicks.Yoga.Portal.Validation.StyleOrganisations
{
    public class OrganisationUpdateValidator : AbstractValidator<OrganisationUpdateModel>
    {
        public OrganisationUpdateValidator()
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

            RuleFor(x => x.Location)
                .Must(x => x.HasValue).WithLocalizedMessage(() => Shared.LocationMissing);

            RuleFor(x => x.Telephone)
                .NotEmpty().WithLocalizedMessage(() => Shared.TelephoneEmpty)
                .Length(0, 100).WithLocalizedMessage(() => Shared.TextTooLong);

            RuleFor(x => x.EmailAddress)
                .NotEmpty().WithLocalizedMessage(() => Shared.EmailAddressEmpty)
                .EmailAddress().WithLocalizedMessage(() => Shared.EmailAddressInvalid);

            RuleFor(e => e.Description)
                .NotEmpty().WithLocalizedMessage(() => Shared.DescriptionEmpty)
                .Length(0, 1200).WithLocalizedMessage(() => Shared.TextTooLong);
        }
    }
}