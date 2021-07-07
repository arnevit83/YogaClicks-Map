namespace Clicks.Yoga.Domain.Entities
{
    public class VenueService : Entity, INamed
    {
        public string Name { get; set; }

        public int DisplayOrder { get; set; }
    }
}