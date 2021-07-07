using System;
using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Emails;
using Postal;

namespace Clicks.Yoga.Domain.Services
{
    public class EntityService : ServiceBase, IEntityService
    {
        private readonly Dictionary<EntityReference, EntityHandle> _handleCache = new Dictionary<EntityReference, EntityHandle>();

        private readonly Dictionary<IEntity, EntityHandle> _transientHandleCache = new Dictionary<IEntity, EntityHandle>();

        public EntityService(
            IEntityContext entityContext)
            : base(entityContext, null) {}

        public EntityType GetEntityType(int id)
        {
            return EntityContext.EntityTypes.Get(e => e.Id == id);
        }

        public EntityType GetEntityType(string name)
        {
            return EntityContext.EntityTypes.Get(e => e.Name.ToLower() == name.ToLower());
        }

        public EntityHandle GetEntityHandle(int id)
        {
            return EntityContext.EntityHandles.Get(id);
        }

        public EntityHandle GetEntityHandle(IEntityHandle entity)
        {
            if (entity is EntityHandle) return (EntityHandle)entity;

            if (entity.IsTransient)
            {
                if (_transientHandleCache.ContainsKey(entity)) return _transientHandleCache[entity];
                return null;
            }

            return GetEntityHandle(entity.Id, entity.GetEntityTypeName());
        }

        public EntityHandle GetEntityHandle(int id, string typeName)
        {
            return GetEntityHandle(new EntityReference(id, typeName));
        }

        public EntityHandle GetEntityHandle(EntityReference reference)
        {
            if (_handleCache.ContainsKey(reference)) return _handleCache[reference];

            var handle = EntityContext.EntityHandles.Get(h => h.EntityId == reference.EntityId && h.EntityType.Name == reference.EntityTypeName);

            if (handle != null) _handleCache[reference] = handle;

            return handle;
        }

        public T GetEntity<T>(EntityHandle handle) where T : class
        {
            return GetEntity<T>(handle.EntityId, handle.EntityType);
        }

        public T GetEntity<T>(int id, string typeName) where T : class
        {
            var type = GetEntityType(typeName);
            return GetEntity<T>(id, type);
        }

        public T GetEntity<T>(int id, EntityType type) where T : class
        {
            var repository = EntityContext.GetRepository(type.GetSystemType());

            if (repository == null)
                throw new UnknownEntityTypeException();

            return repository.Get(id) as T;
        }

        public T GetEntity<T>(EntityReference reference) where T : class
        {
            return GetEntity<T>(reference.EntityId, reference.EntityTypeName);
        }

        public EntityHandle EnsureEntityHandle(IEntityHandle entity)
        {
            var handle = GetEntityHandle(entity);

            if (handle != null) return handle;

            var typeName = entity.GetEntityTypeName();
            var entityType = EntityContext.EntityTypes.Get(e => e.Name == typeName);

            handle = new EntityHandle
            {
                EntityId = entity.Id,
                EntityType = entityType,
                CreationTime = DateTime.Now,
                ModificationTime = DateTime.Now
            };

            if (entity.IsTransient)
            {
                EntityContext.RegisterTransientEntityDependency(handle, e => e.EntityId, entity);
                _transientHandleCache[entity] = handle;
            }
            else
            {
                var reference = new EntityReference(handle.EntityId, handle.EntityType.Name);
                _handleCache[reference] = handle;
            }

            EntityContext.EntityHandles.Add(handle);

            UpdateEntityHandle(handle, entity);

            return handle;
        }

        public EntityHandle EnsureEntityHandle(int id, string typeName)
        {
            var entity = GetEntity<IEntityHandle>(id, typeName);

            if (entity == null)
                throw new ArgumentOutOfRangeException("id");

            return EnsureEntityHandle(entity);
        }

        public EntityHandle EnsureEntityHandle(EntityReference reference)
        {
            return EnsureEntityHandle(reference.EntityId, reference.EntityTypeName);
        }

        public void UpdateEntityHandle(IEntityHandle entity)
        {
            var handle = GetEntityHandle(entity);
            UpdateEntityHandle(handle, entity);
        }

        public EntityHandleInvite InviteEntity(IEntityHandle entity, User user, string emailAddress)
        {
            if (entity == null) return null;
            if (string.IsNullOrEmpty(emailAddress)) return null;

            var handle = EnsureEntityHandle(entity);

            if (handle.Owner != null || EntityContext.EntityHandleInvites.Any(e => e.EmailAddress.ToLower() == emailAddress.ToLower()))
            {
                return null;
            }

            var invite = new EntityHandleInvite();

            invite.EmailAddress = emailAddress;
            invite.EntityHandle = handle;
            invite.User = user;

            EntityContext.EntityHandleInvites.Add(invite);

            new InviteEntityEmail { Invite = invite }.Send();
            
            return invite;
        }

        private void UpdateEntityHandle(EntityHandle handle, IEntityHandle entity)
        {
            if (handle == null) return;

            handle.Update(entity);

            var parents = new List<IEntity> { entity };

            do
            {
                entity = entity.GetParentEntity() as IEntityHandle;

                if (entity != null && !parents.Contains(entity))
                {
                    handle.Parent = EnsureEntityHandle(entity);
                    handle = handle.Parent;
                }
            }
            while (entity != null);
        }
    }
}