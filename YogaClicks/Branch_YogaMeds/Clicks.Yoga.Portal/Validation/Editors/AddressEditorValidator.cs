using Clicks.Yoga.Portal.Models.Editors;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;

namespace Clicks.Yoga.Portal.Validation.Editors
{
    public class AddressEditorValidator : AbstractValidator<AddressEditorModel>
    {
        public AddressEditorValidator()
        {
            RuleFor(e => e.Line1)
                .NotEmpty().WithLocalizedMessage(() => Shared.AddressEmpty)
                .Length(0, 100).WithLocalizedMessage(() => Shared.TextTooLong);

            RuleFor(e => e.City)
                .NotEmpty().WithLocalizedMessage(() => Shared.CityEmpty)
                .Length(0, 100).WithLocalizedMessage(() => Shared.TextTooLong);

            RuleFor(e => e.Postcode)
                .NotEmpty().WithLocalizedMessage(() => Shared.PostcodeEmpty)
                .Length(0, 20).WithLocalizedMessage(() => Shared.TextTooLong);

            RuleFor(e => e.Country.Selection)
                .NotNull().WithLocalizedMessage(() => Shared.CountryNull);
        }
    }
}