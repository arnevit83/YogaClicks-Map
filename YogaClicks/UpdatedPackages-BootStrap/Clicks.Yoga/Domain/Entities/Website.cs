namespace Clicks.Yoga.Domain.Entities
{
    public class Website : Entity
    {
        public Website()
        {
        }

        public Website(string url) : this()
        {
            Url = url;
        }

        public string Url { get; set; }
    }
}