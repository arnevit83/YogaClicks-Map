using System;
using System.Collections.Generic;
using System.Linq;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Media
{
    public class MediaPostPartialModel
    {
        public MediaPostPartialModel(IEnumerable<MediaResource> resources, int? width)
        {
            if (resources == null)
                throw new ArgumentNullException("resources");

            Resources = resources.Select(r => new MediaResourceModel(r)).ToList();
            Types = Resources.Select(r => r.Type).GroupBy(t => t.Tag).Select(g => g.First()).ToList();
            Width = width;
        }

        public IList<MediaResourceTypeModel> Types { get; private set; }

        public int? Width { get; private set; }

        private IList<MediaResourceModel> Resources { get; set; }

        public string GetPartial(MediaResourceTypeModel type)
        {
            return string.Format("~/Views/Media/PostPartials/{0}.cshtml", type.Tag);
        }

        public IList<MediaResourceModel> GetResources(MediaResourceTypeModel type)
        {
            if (type == null)
                throw new ArgumentNullException("type");

            return Resources.Where(r => r.Type.Id == type.Id).ToList();
        }
    }
}