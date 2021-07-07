using Clicks.Yoga.Portal.Models.Reviews;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;

namespace Clicks.Yoga.Portal.Validation.Reviews
{
    public class ReviewReportValidator : AbstractValidator<ReviewReportModel>
    {
        public ReviewReportValidator()
        {
            RuleFor(m => m.TermsAccepted)
                .Equal(true).WithLocalizedMessage(() => Shared.TermsNotAccepted);
        }
    }
}