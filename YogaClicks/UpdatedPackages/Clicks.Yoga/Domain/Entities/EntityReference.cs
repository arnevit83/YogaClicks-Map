namespace Clicks.Yoga.Domain.Entities
{
    public struct EntityReference
    {
        private int _entityId;
        private string _entityTypeName;

        public EntityReference(int entityId, string entityTypeName)
        {
            _entityId = entityId;
            _entityTypeName = entityTypeName;
        }

        public int EntityId
        {
            get { return _entityId; }
            set { _entityId = value; }
        }

        public string EntityTypeName
        {
            get { return _entityTypeName; }
            set { _entityTypeName = value; }
        }
    }
}