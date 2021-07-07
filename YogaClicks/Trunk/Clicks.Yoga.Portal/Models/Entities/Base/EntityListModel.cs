using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class EntityListModel
    {
        public EntityListModel(INamed entity)
        {
            Id = entity.Id;
            Name = entity.Name;
        }

        public int Id { get; private set; }

        public string Name { get; private set; }
    }
}