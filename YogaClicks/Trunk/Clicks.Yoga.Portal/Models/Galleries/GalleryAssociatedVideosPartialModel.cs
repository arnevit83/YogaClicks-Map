using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;

namespace Clicks.Yoga.Portal.Models.Galleries
{
    public class GalleryAssociatedVideosPartialModel
    {
        public GalleryAssociatedVideosPartialModel(int entityId, EntityType entityType, IEnumerable<Video> videos, GalleryPermissions permissions, int totalResults, int page, int resultsPerPage)
        {
            EntityId = entityId;
            EntityType = new EntityTypeModel(entityType);
            VideoIds = new List<int>();
            Permissions = permissions;

            TotalResults = totalResults;
            TotalPages = (int)Math.Ceiling(totalResults / (decimal)resultsPerPage);
            CurrentPage = page;
            FirstResult = ((page - 1) * resultsPerPage) + 1;
            LastResult = page * resultsPerPage;

            if (LastResult > TotalResults)
                LastResult = TotalResults;

            foreach (var video in videos.Skip(FirstResult - 1).Take(resultsPerPage))
            {
                VideoIds.Add(video.Id);
            }
        }

        public int TotalResults { get; set; }

        public int FirstResult { get; set; }

        public int LastResult { get; set; }

        public int ResultsPerPage { get; set; }

        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int EntityId { get; private set; }

        public EntityTypeModel EntityType { get; private set; }

        public IList<int> VideoIds { get; private set; }

        public GalleryPermissions Permissions { get; private set; }

        public SelectList LengthOptions
        {
            get
            {
                return new SelectList(Video.LengthOptions.Select(s => new SelectListItem { Value = s, Text = s }), "Value", "Text");
            }
        }
    }
}