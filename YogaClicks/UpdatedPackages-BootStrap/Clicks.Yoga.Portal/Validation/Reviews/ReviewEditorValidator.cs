using System.Text.RegularExpressions;
using Clicks.Yoga.Portal.Models.Reviews;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;

namespace Clicks.Yoga.Portal.Validation.Reviews
{
    public class EditorValidator<T> : AbstractValidator<T> where T : ReviewEditorModel
    {
        public EditorValidator()
        {
            RuleFor(e => e.Headline)
                .NotEmpty().WithLocalizedMessage(() => Resources.Errors.Reviews.HeadlineEmpty)
                .Length(0, 100).WithLocalizedMessage(() => Shared.TextTooLong);

            RuleFor(e => e.Body)
                .NotEmpty().WithLocalizedMessage(() => Resources.Errors.Reviews.BodyEmpty)
                .Must(e => e == null || Regex.Split(e, @"\s+").Length <= 300).WithLocalizedMessage(() => Resources.Errors.Reviews.BodyTooLong)
                .Length(0, 1200).WithLocalizedMessage(() => Shared.TextTooLong);

            RuleFor(e => e.Rating)
                .GreaterThan(0).WithLocalizedMessage(() => Resources.Errors.Reviews.RatingZero);

            RuleFor(e => e.TermsAccepted)
                .Equal(true).WithLocalizedMessage(() => Resources.Errors.Shared.TermsNotAccepted);
        }
    }
}