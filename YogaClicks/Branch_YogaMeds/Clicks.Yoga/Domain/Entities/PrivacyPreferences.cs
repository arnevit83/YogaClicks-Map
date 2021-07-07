namespace Clicks.Yoga.Domain.Entities
{
    public class PrivacyPreferences : Entity
    {
        public static readonly PrivacyPreferences Default = new PrivacyPreferences
        {
            SharingPermitted = true
        };

        public bool SharingPermitted { get; set; }
    }
}