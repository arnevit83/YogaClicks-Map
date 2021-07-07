using Clicks.Yoga.Portal.Models.Editors;
using Clicks.Yoga.Portal.Resources.Errors;
using Clicks.Yoga.Web;
using FluentValidation;

namespace Clicks.Yoga.Portal.Validation.Editors
{
    public class WebsiteEditorValidator : AbstractValidator<WebsiteEditorModel>
    {
        public WebsiteEditorValidator()
        {
            RuleFor(m => m.Url)
                .Must(WebUtility.IsValidHttpUrl).When(m => !string.IsNullOrEmpty(m.Url)).WithLocalizedMessage(() => Shared.WebsiteInvalid)
                .Length(0, 100).WithLocalizedMessage(() => Shared.TextTooLong);
        }
    }
}