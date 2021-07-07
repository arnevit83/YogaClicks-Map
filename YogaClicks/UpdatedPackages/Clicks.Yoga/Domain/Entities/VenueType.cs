namespace Clicks.Yoga.Domain.Entities
{
    public class VenueType : Entity, INamed
    {
        public string Name { get; set; }

        public bool IsResidential { get; set; }
    }
}