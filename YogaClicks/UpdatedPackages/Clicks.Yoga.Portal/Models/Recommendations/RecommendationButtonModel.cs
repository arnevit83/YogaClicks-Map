namespace Clicks.Yoga.Portal.Models.Recommendations
{
    public class RecommendationButtonModel
    {
        public RecommendationButtonModel(int entityId, string entityTypeName)
        {
            EntityId = entityId;
            EntityTypeName = entityTypeName;
        }

        public int EntityId { get; private set; }

        public string EntityTypeName { get; private set; }
    }
}