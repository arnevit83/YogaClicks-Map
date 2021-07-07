using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.SignUp
{
    public class SignUpImages
    {
        public SignUpProfileBannerEditorModel ProfileBanner { get; set; }

        public SignUpProfileImageEditorModel Image { get; set; }

        public string ColorCssClass { get; set; }
    }
}