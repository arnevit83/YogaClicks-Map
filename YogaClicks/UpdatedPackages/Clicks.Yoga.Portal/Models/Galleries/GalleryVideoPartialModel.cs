using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Editors;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Galleries
{
    public class GalleryVideoPartialModel
    {
        public GalleryVideoPartialModel(
            int entityId,
            string entityTypeName,
            Video video,
            GalleryPermissions permissions,
            IEnumerable<AbilityLevel> abilityLevels,
            IEnumerable<EntityHandle> associateOptions)
        {
            EntityId = entityId;
            EntityTypeName = entityTypeName;
            Video = new VideoModel(video);
            Permissions = permissions;

            AssociateHandles = new EntityHandleChooserModel(video.Associations.Select(a => a.Handle));

            if (associateOptions.Any())
            {
                AssociateHandles.PopulateMetadata(associateOptions);    
            }

            AbilityLevelOptions = new SelectList(
                abilityLevels,
                "Id",
                "Name",
                video.AbilityLevel == null ? "" : Convert.ToString(video.AbilityLevel.Id)
            );
        }

        public int EntityId { get; private set; }

        public string EntityTypeName { get; private set; }

        public VideoModel Video { get; private set; }

        public SelectList AbilityLevelOptions { get; private set; }

        public EntityHandleChooserModel AssociateHandles { get; private set; }

        public GalleryPermissions Permissions { get; private set; }

        public SelectList LengthOptions
        {
            get
            {
                return new SelectList(
                    Clicks.Yoga.Domain.Entities.Video.LengthOptions.Select(s => new SelectListItem { Value = s, Text = s }),
                    "Value",
                    "Text",
                    Video.Length);
            }
        }
    }
}