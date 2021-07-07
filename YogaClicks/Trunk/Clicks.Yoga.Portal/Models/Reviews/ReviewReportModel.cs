namespace Clicks.Yoga.Portal.Models.Reviews
{
    public class ReviewReportModel
    {
        public ReviewReportModel() {}

        public ReviewReportModel(int reviewId, string reason)
        {
            ReviewId = reviewId;
            Reason = reason;
        }

        public int ReviewId { get; set; }

        public string Reason { get; set; }

        public string Explanation { get; set; }

        public bool TermsAccepted { get; set; }
    }
}