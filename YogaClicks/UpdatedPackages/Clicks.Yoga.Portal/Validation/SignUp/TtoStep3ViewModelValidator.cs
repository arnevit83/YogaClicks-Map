using Clicks.Yoga.Portal.Models.SignUp;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;

namespace Clicks.Yoga.Portal.Validation.SignUp
{
    public class TtoStep3ViewModelValidator : AbstractValidator<TtoStep3ViewModel>
    {
        public TtoStep3ViewModelValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithLocalizedMessage(() => Shared.ForenameEmpty)
                .Length(1, 100).WithLocalizedMessage(() => Shared.TextTooLong);

            RuleFor(e => e.Founder)
                .NotEmpty().WithLocalizedMessage(() => Shared.SurnameEmpty)
                .Length(1, 100).WithLocalizedMessage(() => Shared.TextTooLong);

            RuleFor(e => e.Address.Line1)
                //.NotEmpty().WithLocalizedMessage(() => Shared.AddressFirstLineEmpty)
                .Length(0, 100).WithLocalizedMessage(() => Shared.TextTooLong);

            RuleFor(e => e.Address.City)
                .NotEmpty().WithLocalizedMessage(() => Shared.CityEmpty)
                .Length(0, 100).WithLocalizedMessage(() => Shared.TextTooLong);

            RuleFor(e => e.Address.Postcode)
                .NotEmpty().WithLocalizedMessage(() => Shared.PostcodeEmpty)
                .Length(0, 100).WithLocalizedMessage(() => Shared.TextTooLong);

            RuleFor(e => e.Address.Country.Selection)
                .NotNull().WithLocalizedMessage(() => Shared.CountryNull);
        }
    }
}