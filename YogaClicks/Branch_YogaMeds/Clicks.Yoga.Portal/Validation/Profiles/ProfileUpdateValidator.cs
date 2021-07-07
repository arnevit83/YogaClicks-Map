using System;
using Clicks.Yoga.Portal.Models.Editors;
using Clicks.Yoga.Portal.Models.Profiles;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;
using Local = Clicks.Yoga.Portal.Resources.Errors.Profiles;

namespace Clicks.Yoga.Portal.Validation.Profiles
{
    public class ProfileUpdateValidator : AbstractValidator<ProfileUpdateModel>
    {
        public ProfileUpdateValidator()
        {
            RuleFor(e => e.Forename)
                .NotEmpty().WithLocalizedMessage(() => Shared.ForenameEmpty)
                .Length(0, 100).WithLocalizedMessage(() => Shared.TextTooLong);

            RuleFor(e => e.Surname)
                .NotEmpty().WithLocalizedMessage(() => Shared.SurnameEmpty)
                .Length(0, 100).WithLocalizedMessage(() => Shared.TextTooLong);

            RuleFor(e => e.BirthDate)
                .Must(new Func<DateEditorModel, bool>(v => v == null || v.IsOverEighteenYearsAgo)).WithLocalizedMessage(() => Shared.DateInvalid);

            RuleFor(e => e.Location)
                .Must(e => e.HasValue)
                .WithLocalizedMessage(() => Local.PersonalLocationEmpty);

            RuleFor(e => e.EmailAddress)
                .EmailAddress().WithLocalizedMessage(() => Shared.EmailAddressInvalid).Unless(e => string.IsNullOrEmpty(e.EmailAddress));
        }
    }
}