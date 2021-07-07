namespace Clicks.Yoga.Portal.Models.Shared
{
    public class CloseModalModel
    {
        public CloseModalModel(string destination, string reason)
        {
            Destination = destination;
            Reason = reason;
        }

        public string Destination { get; set; }
        
        public string Reason { get; set; }

        public string ViewPath
        {
            get { return string.Format("~/Views/Shared/CloseModalReasons/{0}.cshtml", Reason); }
        }
    }
}