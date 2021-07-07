using System.Linq;
using Clicks.Yoga.Portal.Models.Activities;
using Clicks.Yoga.Portal.Resources.Errors;
using FluentValidation;
using Local = Clicks.Yoga.Portal.Resources.Errors.Activities;

namespace Clicks.Yoga.Portal.Validation.Activities
{
    public class ActivityCreateClassStep3Validator : AbstractValidator<ActivityCreateClassStep3Model>
    {
        public ActivityCreateClassStep3Validator()
        {
        }
    }
}