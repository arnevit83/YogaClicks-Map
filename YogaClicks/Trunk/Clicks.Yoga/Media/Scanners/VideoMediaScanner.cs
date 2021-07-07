using System;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;

namespace Clicks.Yoga.Media.Scanners
{
    public class VideoMediaScanner : IMediaResourceScanner
    {
        public VideoMediaScanner(IVideoService videoService)
        {
            VideoService = videoService;
        }

        private IVideoService VideoService { get; set; }

        public void Scan(MediaResource resource)
        {
            if (resource == null)
                throw new ArgumentNullException("resource");

            int id;

            if (!int.TryParse(resource.Identifier, out id))
            {
                throw new UnsupportedMediaException();
            }
            
            var video = VideoService.GetVideo(id);

            resource.EntityId = id;
            resource.Content = video.EmbedHtml;
        }

        public void Commit(MediaResource resource) { }
    }
}