using System;
using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.Registration
{
    public class RegisterAccountModel
    {
        public RegisterAccountModel() : this(false) {}

        public RegisterAccountModel(bool professional)
        {
            Professional = professional;
        }

        public bool Professional { get; set; }

        public string Forename { get; set; }

        public string Surname { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string PasswordConfirmation { get; set; }

        public bool TermsAccepted { get; set; }

        public bool NewsletterOptOut { get; set; }

        public bool ThirdPartyOptOut { get; set; }

        public CreateAccountView View { get; set; }
    }

    public enum CreateAccountView
    {
        Standard,
        Popup
    }
}