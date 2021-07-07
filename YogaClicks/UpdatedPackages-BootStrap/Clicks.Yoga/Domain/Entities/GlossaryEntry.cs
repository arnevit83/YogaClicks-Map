namespace Clicks.Yoga.Domain.Entities
{
    public class GlossaryEntry : Entity, IFanable
    {
        public string Term { get; set; }

        public string Definition { get; set; }

        string IEntityHandle.Name
        {
            get { return Term; }
        }

        Image IEntityHandle.Image
        {
            get { return null; }
        }

        string IEntityHandle.Location
        {
            get { return null; }
        }

        User IEntityHandle.Owner
        {
            get { return null; }
        }

        bool IEntityHandle.Active
        {
            get { return true; }
        }
    }
}
