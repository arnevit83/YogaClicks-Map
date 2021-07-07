using System.Collections.Generic;
using System.Runtime.Serialization;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Editors
{
    [DataContract]
    public class SingleEntitySelectorModel<TEntity, TModel> : EntitySelectorModel<TEntity, TModel>
        where TEntity : Entity
        where TModel : EntityModel<TEntity>, new()
    {
        public SingleEntitySelectorModel() {} 

        public SingleEntitySelectorModel(TEntity entity)
        {
            if (entity != null) Selection = entity.Id;
        }

        public SingleEntitySelectorModel(TEntity entity, IEnumerable<TEntity> options)
            : this(entity)
        {
            PopulateMetadata(options);
        }

        public int Id
        {
            get { return Selected ? Selection.Value : 0; }
        }

        [DataMember]
        public int? Selection { get; set; }

        public bool Selected
        {
            get { return Selection.HasValue; }
        }

        public TModel SelectedEntity
        {
            get { return Selected ? Entities[Id] : null; }
        }

        protected override bool EntitySelected(TEntity entity)
        {
            return Id == entity.Id;
        }
    }
}