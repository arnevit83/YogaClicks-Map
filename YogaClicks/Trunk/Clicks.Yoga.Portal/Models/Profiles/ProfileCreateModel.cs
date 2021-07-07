
namespace Clicks.Yoga.Portal.Models.Profiles
{
    public class ProfileCreateModel
    {
        public ProfileCreateModel() { }

        public ProfileCreateModel(bool hasTeacher, WizardStatus status) : this()
        {
            HasTeacher = hasTeacher;
            Status = status;

            if (status == WizardStatus.None)
            {
                if (!hasTeacher)
                {
                    Type = ProfileCreateType.Teacher;
                }
                else
                {
                    Type = ProfileCreateType.Venue;
                }
            }
        }

        public bool HasTeacher { get; set; }

        public WizardStatus Status { get; set; }

        public ProfileCreateType Type { get; set; }
    }

    public enum WizardStatus
    {
        None,
        First,
        Subsequent
    }
}