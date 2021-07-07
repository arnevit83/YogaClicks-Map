using Clicks.Yoga.Portal.Models.Messaging;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;
using Local = Clicks.Yoga.Portal.Resources.Errors.Messaging;

namespace Clicks.Yoga.Portal.Validation.Messaging
{
    public class MessagingSendValidator : AbstractValidator<MessagingSendModel>
    {
        public MessagingSendValidator()
        {
            RuleFor(m => m.Friends)
                .Must(m => m.Ids.Count > 0).WithLocalizedMessage(() => Local.RecipientsEmpty)
                .When(m => m.Recipient == null);

            RuleFor(m => m.Recipient)
                .NotNull().WithLocalizedMessage(() => Local.RecipientsEmpty)
                .When(m => m.Friends.Ids.Count == 0);

            RuleFor(m => m.Content)
                .NotEmpty().WithLocalizedMessage(() => Local.ContentEmpty)
                .Length(0, 1200).WithLocalizedMessage(() => Shared.TextTooLong);
        }
    }
}