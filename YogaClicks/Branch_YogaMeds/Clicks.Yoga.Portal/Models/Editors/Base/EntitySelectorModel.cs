using System.Collections.Generic;
using System.Runtime.Serialization;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Editors
{
    [DataContract]
    public abstract class EntitySelectorModel<TEntity, TModel>
        where TEntity : Entity
        where TModel : EntityModel<TEntity>, new()
    {
        public EntitySelectorModel()
        {
            Options = new List<TModel>();
            AvailableOptions = new List<TModel>();
            Entities = new Dictionary<int, TModel>();
        }

        public IList<TModel> Options { get; private set; }

        public IList<TModel> AvailableOptions { get; private set; } 

        protected IDictionary<int, TModel> Entities { get; private set; }

        protected abstract bool EntitySelected(TEntity entity);

        public virtual void PopulateMetadata(IEnumerable<TEntity> entities)
        {
            Options.Clear();
            AvailableOptions.Clear();
            Entities.Clear();

            foreach (var entity in entities)
            {
                PopulateOption(entity);
            }
        }

        protected virtual void PopulateOption(TEntity entity)
        {
            if (Entities.ContainsKey(entity.Id)) return;

            var model = CreateModel(entity);

            Options.Add(model);
            Entities.Add(entity.Id, model);

            if (!EntitySelected(entity)) AvailableOptions.Add(model);
        }

        protected virtual TModel CreateModel(TEntity entity)
        {
            var model = new TModel();
            model.Populate(entity);
            return model;
        }

        [OnDeserialized]
        private void OnDesirialized(StreamingContext context)
        {
            Options = new List<TModel>();
            AvailableOptions = new List<TModel>();
            Entities = new Dictionary<int, TModel>();
        }
    }
}