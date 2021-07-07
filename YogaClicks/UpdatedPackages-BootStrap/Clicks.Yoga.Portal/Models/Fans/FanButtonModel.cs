namespace Clicks.Yoga.Portal.Models.Fans
{
    public class FanButtonModel
    {
        public FanButtonModel(int entityId, string entityTypeName, bool isFanned, bool canFan  )
        {
            EntityId = entityId;
            EntityTypeName = entityTypeName;
            IsFanned = isFanned;
            CanFan = canFan;
        }

        public int EntityId { get; set; }

        public string EntityTypeName { get; set; }

        public bool IsFanned { get; set; }

        public bool CanFan { get; set; }

        public bool NewsFeed { get; set; }

        public int FanCount { get; set; }
    }
}