using System.Configuration;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Emails
{
    public class AdminReviewReportEmail : EmailBase
    {
        public AdminReviewReportEmail(Review review, string reason, string explanation)
        {
            Review = review;
            Reason = reason;
            Explanation = explanation;
        }

        public Review Review { get; set; }

        public string Reason { get; set; }

        public string Explanation { get; set; }

        public override string To
        {
            get
            {
                switch (Reason)
                {
                    case "SPAM":
                        return ConfigurationManager.AppSettings["Clicks.Yoga.Emails.Recipients.Spam"];
                    default:
                        return ConfigurationManager.AppSettings["Clicks.Yoga.Emails.Recipients.Report"];

                }
            }
        }
    }
}