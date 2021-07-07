namespace Clicks.Yoga.Domain.Entities
{
    public class TeacherService : Entity, INamed
    {
        public string Name { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsClass { get; set; }
    }
}