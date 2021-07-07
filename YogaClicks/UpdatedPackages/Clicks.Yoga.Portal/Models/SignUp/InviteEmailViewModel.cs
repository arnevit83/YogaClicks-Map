using Microsoft.Web.Mvc;

namespace Clicks.Yoga.Portal.Models.SignUp
{
    public class InviteEmailViewModel
    {
        public string ToEmail { get; set; }

        public string SenderEmail { get; set; }
        
        public string Message { get; set; }
    }
}