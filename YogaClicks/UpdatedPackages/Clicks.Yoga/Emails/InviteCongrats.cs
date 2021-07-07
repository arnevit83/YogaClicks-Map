using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Emails
{
    public class InviteCongrats : EmailBase
    {
        public string InviteFromName { get; set; }

        public string InviteToEmail { get; set; }

        public string InviteToName { get; set; }

        public override string To
        {
            get { return InviteToEmail; }
        }
    }
}