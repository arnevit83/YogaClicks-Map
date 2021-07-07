namespace Clicks.Yoga.Domain.Entities
{
    public class Quote : Entity, IFanable
    {
        public string Author { get; set; }

        public string Quotation { get; set; }

        string IEntityHandle.Name
        {
            get { return "'" + Quotation + "' - " + Author; }
        }

        Image IEntityHandle.Image
        {
            get { return null; }
        }

        User IEntityHandle.Owner
        {
            get { return null; }
        }

        string IEntityHandle.Location
        {
            get { return null; }
        }

        bool IEntityHandle.Active
        {
            get { return true; }
        }
    }
}
