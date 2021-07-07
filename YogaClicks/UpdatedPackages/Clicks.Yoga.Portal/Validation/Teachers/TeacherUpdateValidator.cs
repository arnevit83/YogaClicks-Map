using Clicks.Yoga.Portal.Models.Teachers;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;

namespace Clicks.Yoga.Portal.Validation.Teachers
{
    public class TeacherUpdateValidator : AbstractValidator<TeacherUpdateModel>
    {
        public TeacherUpdateValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithLocalizedMessage(() => Shared.NameEmpty)
                .Length(0, 100).WithLocalizedMessage(() => Shared.TextTooLong);

            //RuleFor(e => e.Telephone)
            //    .NotEmpty().WithLocalizedMessage(() => Shared.TelephoneEmpty)
            //    .Length(0, 100).WithLocalizedMessage(() => Shared.TextTooLong);

            RuleFor(e => e.EmailAddress)
                .NotEmpty().WithLocalizedMessage(() => Shared.EmailAddressEmpty)
                .EmailAddress().WithLocalizedMessage(() => Shared.EmailAddressInvalid);
        }
    }
}