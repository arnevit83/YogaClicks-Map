using System.Collections.Generic;
using Clicks.Yoga.Domain.Entities;

namespace Clicks.Yoga.Domain.Services
{
    public interface IMediaService
    {
        IList<MediaResource> GetResources(IEnumerable<int> ids);
        MediaResource ScanResource(string uri);
        MediaResource ScanWebsiteImages(string uri);
        MediaResource CommitResource(string uri);
    }
}