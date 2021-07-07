using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public interface IEntityService
    {
        EntityType GetEntityType(int id);
        EntityType GetEntityType(string name);
        EntityHandle GetEntityHandle(int id);
        EntityHandle GetEntityHandle(IEntityHandle entity);
        EntityHandle GetEntityHandle(int id, string typeName);
        EntityHandle GetEntityHandle(EntityReference reference);
        T GetEntity<T>(EntityHandle handle) where T : class;
        T GetEntity<T>(int id, string typeName) where T : class;
        T GetEntity<T>(int id, EntityType type) where T : class;
        T GetEntity<T>(EntityReference reference) where T : class;
        EntityHandle EnsureEntityHandle(IEntityHandle entity);
        EntityHandle EnsureEntityHandle(int id, string typeName);
        EntityHandle EnsureEntityHandle(EntityReference reference);
        void UpdateEntityHandle(IEntityHandle entity);
        EntityHandleInvite InviteEntity(IEntityHandle entity, User user, string emailAddress);
    }
}