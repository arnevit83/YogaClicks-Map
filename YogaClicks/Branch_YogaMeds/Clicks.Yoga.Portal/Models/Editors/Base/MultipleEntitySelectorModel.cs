using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Editors
{
    [DataContract]
    public class MultipleEntitySelectorModel<TEntity, TModel> : EntitySelectorModel<TEntity, TModel>
        where TEntity : Entity
        where TModel : EntityModel<TEntity>, new()
    {
        [DataMember]
        private readonly EntityValueDictionary<bool> _selection;

        public MultipleEntitySelectorModel()
        {
            _selection = new EntityValueDictionary<bool>();
        }

        public MultipleEntitySelectorModel(IEnumerable<TEntity> entities) : this()
        {
            _selection = new EntityValueDictionary<bool>();

            foreach (var entity in entities)
            {
                Selection[entity.Id] = true;
            }
        }

        public MultipleEntitySelectorModel(IEnumerable<TEntity> selected, IEnumerable<TEntity> options) : this(selected)
        {
            PopulateMetadata(options);
        }

        public IList<int> Ids
        {
            get { return Selection.Where(p => p.Value).Select(p => Convert.ToInt32(p.Key)).ToList(); }
        }

        public IDictionary<object, bool> Selection
        {
            get { return _selection; }
        }

        public IList<TModel> SelectedEntities
        {
            get
            {
                var list = new List<TModel>();

                foreach (var pair in Selection)
                {
                    var key = Convert.ToInt32(pair.Key);

                    if (Selection[key] && Entities.ContainsKey(key))
                    {
                        list.Add(Entities[key]);
                    }
                }

                return list;
            }
        }

        protected override bool EntitySelected(TEntity entity)
        {
            return Selection.ContainsKey(entity.Id) && Selection[entity.Id];
        }

        protected override void PopulateOption(TEntity entity)
        {
            base.PopulateOption(entity);

            if (!Selection.ContainsKey(entity.Id))
            {
                Selection.Add(entity.Id, false);
            }
        }

        protected void PopulateCollection(ICollection<TEntity> collection, Func<int, TEntity> createEntity)
        {
            PopulateCollection(collection, createEntity, e => e);
        }

        protected void PopulateCollection<TElement>(ICollection<TElement> collection, Func<int, TElement> createElement, Func<TElement, TEntity> getEntity)
        {
            foreach (var element in collection.ToList())
            {
                var entity = getEntity(element);
                if (!Ids.Contains(entity.Id)) collection.Remove(element);
            }

            foreach (var id in Ids)
            {
                if (!collection.Any(e => getEntity(e).Id == id)) collection.Add(createElement(id));
            }
        }
    }
}