using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;

namespace Clicks.Yoga.Portal.Validation.Editors
{
    public class ImageUploadValidator : AbstractValidator<ImageUploadModel>
    {
        public ImageUploadValidator(IImageService imageService)
        {
            RuleFor(e => e.PostedFile)
                .Must(f => imageService.ValidImageStream(f.InputStream))
                .When(m => m.PostedFile != null)
                .WithLocalizedMessage(() => Shared.InvalidImage);
        }
    }
}