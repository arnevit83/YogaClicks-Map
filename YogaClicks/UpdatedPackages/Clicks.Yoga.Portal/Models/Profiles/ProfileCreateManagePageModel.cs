namespace Clicks.Yoga.Portal.Models.Profiles
{
    public class ProfileCreateManagePageModel
    {
        public bool isNewUser { get; set; }
        public ManagePage? ManageBy { get; set; }

        public enum ManagePage
        {
            Personal,
            New
        }
    }
}