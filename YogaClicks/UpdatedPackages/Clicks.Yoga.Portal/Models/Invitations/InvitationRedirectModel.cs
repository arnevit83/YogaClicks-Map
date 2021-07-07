namespace Clicks.Yoga.Portal.Models.Invitations
{
    public class InvitationRedirectModel
    {
        public InvitationRedirectModel(string destination)
        {
            Destination = destination;
        }

        public string Destination { get; set; }
    }
}