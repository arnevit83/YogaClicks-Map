using System;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Portal.Models.Entities
{
    public class WebsiteModel : EntityModel<Website>
    {
        public WebsiteModel(Website entity) : base(entity) {}

        public string Url { get; set; }

        public string Domain { get; set; }

        public override void Populate(Website entity)
        {
            Id = entity.Id;
            Url = entity.Url;

            Uri uri;

            if (Uri.TryCreate(Url, UriKind.Absolute, out uri))
            {
                Domain = uri.Host;
            }
        }
    }
}