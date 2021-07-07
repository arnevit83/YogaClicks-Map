using Clicks.Yoga.Portal.Models.TeacherTrainingCourses;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;

namespace Clicks.Yoga.Portal.Validation.TeacherTrainingCourses
{
    public class CourseCreateBasicValidator : AbstractValidator<CourseCreateBasicModel>
    {
        public CourseCreateBasicValidator()
        {
            RuleFor(e => e.Name)
                .NotEmpty().WithLocalizedMessage(() => Shared.NameEmpty)
                .Length(0, 100).WithLocalizedMessage(() => Shared.TextTooLong);

            RuleFor(e => e.Duration)
                .NotEmpty().WithLocalizedMessage(() => Shared.DurationEmpty);

            RuleFor(e => e.StartDate)
                .Must(e => e.HasValue).WithLocalizedMessage(() => Shared.StartDateEmpty);

            RuleFor(e => e.FinishDate)
                .Must(e => e.HasValue).WithLocalizedMessage(() => Shared.FinishDateEmpty);

            RuleFor(m => m.FinishDate.DateTime.Value)
                .GreaterThanOrEqualTo(m => m.StartDate.DateTime.Value)
                .When(m => m.FinishDate.HasValue && m.StartDate.HasValue)
                .WithLocalizedMessage(() => Shared.StartDateAfterFinishDate);
        }
    }
}