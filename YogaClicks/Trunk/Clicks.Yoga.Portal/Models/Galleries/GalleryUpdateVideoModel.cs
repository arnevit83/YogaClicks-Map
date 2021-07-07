using System;
using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;
using Clicks.Yoga.Portal.Models.Editors;

namespace Clicks.Yoga.Portal.Models.Galleries
{
    public class GalleryUpdateVideoModel
    {
        public GalleryUpdateVideoModel()
        {
            AssociateHandles = new EntityHandleChooserModel();
        }

        public int Id { get; set; }

        public string Description { get; set; }

        public string Length { get; set; }

        public int AbilityLevelId { get; set; }

        public EntityHandleChooserModel AssociateHandles { get; private set; }

        public void PopulateEntity(Video video, IVideoService videoService)
        {
            video.Description = Description;
            video.Length = Length;
            video.IsClass = !string.IsNullOrEmpty(video.Length);
            video.AbilityLevel = videoService.GetAbilityLevel(AbilityLevelId);

            foreach (var pair in AssociateHandles.Selection)
            {
                var handleId = Convert.ToInt32(pair.Key);

                if (pair.Value)
                {
                    videoService.AssociateVideo(video, handleId);
                }
                else
                {
                    videoService.DisassociateVideo(video, handleId);
                }
            }
        }
    }
}