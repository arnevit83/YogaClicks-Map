using Clicks.Yoga.Portal.Models.StyleOrganisations;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;

namespace Clicks.Yoga.Portal.Validation.StyleOrganisations
{
    public class OrganisationCreateStep2Validator : AbstractValidator<OrganisationCreateStep2Model>
    {
        public OrganisationCreateStep2Validator()
        {
            RuleFor(x => x.Location)
                .Must(x => x.HasValue).WithLocalizedMessage(() => Shared.LocationMissing);

/*            RuleFor(x => x.Telephone)
                .NotEmpty().WithLocalizedMessage(() => Shared.TelephoneEmpty)
                .Length(0, 100).WithLocalizedMessage(() => Shared.TextTooLong);
*/
            RuleFor(x => x.EmailAddress)
                .NotEmpty().WithLocalizedMessage(() => Shared.EmailAddressEmpty)
                .EmailAddress().WithLocalizedMessage(() => Shared.EmailAddressInvalid);
        }
    }
}