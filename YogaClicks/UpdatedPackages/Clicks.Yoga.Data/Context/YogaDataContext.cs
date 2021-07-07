using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Data.Metadata.Edm;
using System.Data.Objects;
using System.Linq;
using System.Reflection;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Data
{
    public class YogaDataContext : DbContext
    {
        public delegate void CreatingEntityHandler(object sender, Entity entity);
        public delegate void ModifyingEntityHandler(object sender, Entity entity);
        public delegate void DeletingEntityHandler(object sender, Entity entity);
        public delegate void SavingEntityHandler(object sender, Entity entity);

        public delegate void EntityCreatedHandler(object sender, Entity entity);
        public delegate void EntityModifiedHandler(object sender, Entity entity);
        public delegate void EntityDeletedHandler(object sender, Entity entity);
        public delegate void EntitySavedHandler(object sender, Entity entity);

        public event CreatingEntityHandler CreatingEntity;
        public event ModifyingEntityHandler ModifyingEntity;
        public event DeletingEntityHandler DeletingEntity;
        public event SavingEntityHandler SavingEntity;

        public event EntityCreatedHandler EntityCreated;
        public event EntityModifiedHandler EntityModified;
        public event EntityDeletedHandler EntityDeleted;
        public event EntitySavedHandler EntitySaved;

       public YogaDataContext()
        {
            EntityChanges = new List<EntityChange>();
        }

        protected ObjectContext ObjectContext
        {
            get { return ((IObjectContextAdapter)this).ObjectContext; }
        }

        protected IList<EntityChange> EntityChanges { get; private set; } 

        public override int SaveChanges()
        {
            int result = 0;

            try
            {
                RecordChanges();
                RaiseSavingEvents();

                result = base.SaveChanges();

                RaiseSavedEvents();
            }
            catch (DbEntityValidationException ex)
            {
                RethrowEntityValidationException(ex);
            }
            finally
            {
                EntityChanges.Clear();
            }

            return result;
        }

        protected virtual void RethrowEntityValidationException(DbEntityValidationException ex)
        {
            var parts = new List<string>();

            foreach (var v in ex.EntityValidationErrors)
            {
                var entity = v.Entry.Entity as Entity;
                
                var type = entity == null ? null : entity.GetEntityTypeName();
                var id = entity == null ? 0 : entity.Id;

                foreach (var e in v.ValidationErrors)
                {
                    parts.Add(string.Format(
                        "{0}[{1}].{2}: {3}",
                        type ?? "?",
                        id == 0 ? "?" : Convert.ToString(id),
                        e.PropertyName,
                        e.ErrorMessage));
                }
            }

            var message = string.Format(
                "Failed to save changes because of entity validation errors{0}{1}",
                parts.Count > 0 ? ":" + Environment.NewLine : ".",
                string.Join(Environment.NewLine, parts.ToArray()));

            throw new DbEntityValidationException(message, ex.EntityValidationErrors);
        }

        protected virtual void RecordChanges()
        {
            EntityChanges.Clear();

            // Plain POCO entities added to child collections are not marked as Added in ObjectStateManager
            // so both ChangeTracker and ObjectStateManager need to be checked.

            foreach (var entry in ChangeTracker.Entries().Where(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted))
            {
                RecordEntityChange(entry);
            }

            foreach (var entry in GetChangedObjectStateEntries())
            {
                if (entry.IsRelationship)
                {
                    RecordRelationshipChange(entry);
                }
            }
        }

        protected virtual void RecordEntityChange(DbEntityEntry entry)
        {
            var entity = entry.Entity as Entity;

            if (entity == null) return;

            RecordChange(entity, entry.State);
        }

        protected virtual void RecordRelationshipChange(ObjectStateEntry entry)
        {
            var ends = entry.GetAssociationEnds();

            foreach (var end in ends)
            {
                // Changes on the 1 or 0..1 end of an association don't represent a modification of
                // the entity itself.
                if (end.RelationshipMultiplicity != RelationshipMultiplicity.Many) continue;

                var property = entry.GetNavigationProperty(end);

                if (property == null) continue;

                var entityKey = entry.GetEndEntityKey(end);

                ObjectStateEntry entityEntry;

                // If there is no state entry for the entity key then the entity is probably transient.
                // Association changes to transient entities don't need to be audited.
                if (!ObjectContext.ObjectStateManager.TryGetObjectStateEntry(entityKey, out entityEntry)) continue;

                var entity = entityEntry.Entity as Entity;

                if (entity == null) continue;

                RecordChange(entity, EntityState.Modified);
            }
        }

        protected virtual void RecordChange(Entity entity, EntityState state)
        {
            if (EntityChanges.Any(c => c.Entity == entity)) return;

            EntityChanges.Add(new EntityChange(entity, state));
        }

        protected virtual void RaiseSavingEvents()
        {
            foreach (var change in EntityChanges)
            {
                OnSavingEntity(change.Entity);

                switch (change.State)
                {
                    case EntityState.Added:
                        OnCreatingEntity(change.Entity);
                        break;
                    case EntityState.Modified:
                        OnModifyingEntity(change.Entity);
                        break;
                    case EntityState.Deleted:
                        // This is raised from Repository<T>.Remove
                        // OnDeletingEntity(change.Entity);
                        break;
                }
            }
        }

        internal void RaiseDeletingEvent(Entity entity)
        {
            OnDeletingEntity(entity);
        }

        protected virtual void RaiseSavedEvents()
        {
            foreach (var change in EntityChanges)
            {
                OnEntitySaved(change.Entity);

                switch (change.State)
                {
                    case EntityState.Added:
                        OnEntityCreated(change.Entity);
                        break;
                    case EntityState.Modified:
                        OnEntityModified(change.Entity);
                        break;
                    case EntityState.Deleted:
                        OnEntityDeleted(change.Entity);
                        break;
                }
            }
        }

        protected IEnumerable<ObjectStateEntry> GetChangedObjectStateEntries()
        {
            return ObjectContext.ObjectStateManager.GetObjectStateEntries(~(EntityState.Unchanged | EntityState.Detached));
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           
                foreach (var type in Assembly.GetExecutingAssembly().GetTypes())
                {
                    if (type.IsClass && type.Namespace == "Clicks.Yoga.Data.Mappings" && !type.ContainsGenericParameters)
                    {
            //        try
            //{
                        modelBuilder.Configurations.Add((dynamic)Activator.CreateInstance(type));
                    //        }
                    //catch (Exception ex)
                    //{
                    //    // add a breakpoint here.
                    //    Console.WriteLine("we failed");
                    //}
                }
                }

                modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

                base.OnModelCreating(modelBuilder);
           

        }

        protected virtual void OnCreatingEntity(Entity entity)
        {
            var e = CreatingEntity;
            if (e != null) e(this, entity);
        }

        protected virtual void OnModifyingEntity(Entity entity)
        {
            var e = ModifyingEntity;
            if (e != null) e(this, entity);
        }

        protected virtual void OnDeletingEntity(Entity entity)
        {
            var e = DeletingEntity;
            if (e != null) e(this, entity);
        }

        protected virtual void OnSavingEntity(Entity entity)
        {
            var e = SavingEntity;
            if (e != null) e(this, entity);
        }

        protected virtual void OnEntityCreated(Entity entity)
        {
            var e = EntityCreated;
            if (e != null) e(this, entity);
        }

        protected virtual void OnEntityModified(Entity entity)
        {
            var e = EntityModified;
            if (e != null) e(this, entity);
        }

        protected virtual void OnEntityDeleted(Entity entity)
        {
            var e = EntityDeleted;
            if (e != null) e(this, entity);
        }

        protected virtual void OnEntitySaved(Entity entity)
        {
            var e = EntitySaved;
            if (e != null) e(this, entity);
        }

        protected class EntityChange
        {
            public EntityChange(Entity entity, EntityState state)
            {
                Entity = entity;
                State = state;
            }

            public Entity Entity { get; private set; }

            public EntityState State { get; private set; }
        }
    }
}