using System;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Media
{
    public class MediaPreviewPartialModel
    {
        public MediaPreviewPartialModel(MediaResource resource)
        {
            if (resource == null)
                throw new ArgumentNullException("resource");

            Resource = new MediaResourceModel(resource);
        }

        public MediaResourceModel Resource { get; private set; }

        public string GetPartial()
        {
            return string.Format("~/Views/Media/PreviewPartials/{0}.cshtml", Resource.Type.Tag);
        }
    }
}