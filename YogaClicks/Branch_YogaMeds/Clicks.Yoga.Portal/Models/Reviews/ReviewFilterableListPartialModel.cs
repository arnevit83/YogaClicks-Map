namespace Clicks.Yoga.Portal.Models.Reviews
{
    public class ReviewFilterableListPartialModel
    {
        public ReviewFilterableListPartialModel(ReviewSearchCriteriaModel criteria)
        {
            Criteria = criteria;
        }

        public ReviewSearchCriteriaModel Criteria { get; set; }
    }
}