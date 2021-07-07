namespace Clicks.Yoga.Domain.Entities
{
    public class AbilityLevel : Entity, INamed
    {
        public byte Index { get; set; }

        public string Name { get; set; }

        public bool IsOpen { get; set; }
    }
}
