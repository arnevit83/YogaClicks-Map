using System;
using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Context;
using Clicks.Yoga.Portal.Models.Context;

namespace Clicks.Yoga.Portal.Models.Actors
{
    public class PickerPartialModel
    {
        public PickerPartialModel(string prefix, IPortalSecurityContext context, EntityReference current)
        {
            Prefix = prefix;
            Options = new List<AvailableActorModel>();

            EntityId = current.EntityId > 0 ? current.EntityId : context.CurrentActor.Id;
            EntityTypeName = current.EntityTypeName ?? context.CurrentActor.EntityType.Name;

            PopulateMetadata(context);
        }

        public string Prefix { get; set; }

        public int EntityId { get; set; }

        public string EntityTypeName { get; set; }

        public IList<AvailableActorModel> Options { get; private set; }

        public AvailableActorModel SelectedOption
        {
            get { return Options.FirstOrDefault(Selected); }
            set
            {
                EntityId = value != null ? value.Id : 0;
                EntityTypeName = value != null ? value.EntityType.Name : null;
            }
        }

        public bool Selected(AvailableActorModel option)
        {
            if (option == null) return false;

            return EntityId == option.Id && EntityTypeName == option.EntityType.Name;
        }

        public string PrefixedName(string name)
        {
            return Prefix != null ? Prefix + "." + name : name;
        }

        public void PopulateMetadata(IPortalSecurityContext context)
        {
            if (context == null)
                throw new ArgumentNullException("context");

            foreach (var actor in context.AvailableActors.OrderByDescending(Selected))
            {
                Options.Add(actor);
            }

            if (SelectedOption == null)
            {
                SelectedOption = context.AvailableActors.First();
            }
        }
    }
}