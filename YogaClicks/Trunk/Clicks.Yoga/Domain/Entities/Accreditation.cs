namespace Clicks.Yoga.Domain.Entities
{
    public class Accreditation : Entity
    {
        public virtual AccreditationBody AccreditationBody { get; set; }

        public string Name { get; set; }
    }
}
