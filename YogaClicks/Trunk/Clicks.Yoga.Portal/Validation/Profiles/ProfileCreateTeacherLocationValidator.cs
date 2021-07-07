using Clicks.Yoga.Portal.Models.Profiles;
using FluentValidation;
using Local = Clicks.Yoga.Portal.Resources.Errors.Profiles;

namespace Clicks.Yoga.Portal.Validation.Profiles
{
    public class ProfileCreateTeacherLocationValidator : AbstractValidator<ProfileCreateTeacherLocationModel>
    {
        public ProfileCreateTeacherLocationValidator()
        {
            RuleFor(e => e.Location)
                .Must(e => e.HasValue).WithLocalizedMessage(() => Local.LocationEmpty);
        }
    }
}