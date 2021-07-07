using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Repositories;
using Clicks.Yoga.Media;
using Clicks.Yoga.Media.Scanners;

namespace Clicks.Yoga.Domain.Services
{
    public class MediaService : ServiceBase, IMediaService
    {
        public MediaService(
            IEntityContext entityContext,
            ISecurityContext securityContext,
            IRepository<MediaResource> resourceRepository,
            IRepository<MediaResourceType> resourceTypeRepository,
            IMediaResourceScannerFactory resourceScannerFactory,
            ImageWebMediaScanner imageWebMediaScanner)
            : base(entityContext, securityContext)
        {
            ResourceRepository = resourceRepository;
            ResourceTypeRepository = resourceTypeRepository;
            ResourceScannerFactory = resourceScannerFactory;
            ImageWebMediaScanner = imageWebMediaScanner;
        }

        private IRepository<MediaResource> ResourceRepository { get; set; }

        private IRepository<MediaResourceType> ResourceTypeRepository { get; set; }

        private IMediaResourceScannerFactory ResourceScannerFactory { get; set; }

        private ImageWebMediaScanner ImageWebMediaScanner { get; set; }

        public IList<MediaResource> GetResources(IEnumerable<int> ids)
        {
            if (ids == null)
                throw new ArgumentNullException("ids");

            var resources = new List<MediaResource>();

            foreach (var id in ids)
            {
                var resource = ResourceRepository.Get(id);

                if (resource != null)
                {
                    resources.Add(resource);
                }
            }

            return resources;
        }

        public MediaResource ScanResource(string uri)
        {
            if (uri == null)
                throw new ArgumentNullException("uri");

            var resource = ResourceRepository.Get(r => r.Uri == uri);

            if (resource != null && (resource.ExpiryTime == null || resource.ExpiryTime > DateTime.Now))
            {
                return resource;
            }

            if (resource == null)
            {
                var types = ResourceTypeRepository.OrderBy(t => t.Priority).ToList();

                MediaResourceType type = null;
                string identifier = null;

                foreach (var candidate in types)
                {
                    var match = Regex.Match(uri, candidate.UriPattern, RegexOptions.IgnoreCase);

                    if (match.Success)
                    {
                        type = candidate;
                        identifier = match.Groups.Count > 1 ? match.Groups[1].Value : null;
                        break;
                    }
                }

                if (type == null)
                {
                    throw new UnsupportedMediaException();
                }

                resource = new MediaResource
                {
                    Type = type,
                    Uri = uri,
                    Identifier = identifier
                };
            }

            var scanner = ResourceScannerFactory.GetHandler(resource.Type);

            if (scanner == null)
            {
                throw new UnsupportedMediaException();
            }

            scanner.Scan(resource);

            if (resource.IsTransient)
            {
                ResourceRepository.Add(resource);
            }

            if (resource.Type.ExpirySeconds.HasValue)
            {
                resource.ExpiryTime = DateTime.Now.AddSeconds(resource.Type.ExpirySeconds.Value);
            }

            return resource;
        }

        public MediaResource ScanWebsiteImages(string uri)
        {
            var resource = ScanResource(uri);

            ImageWebMediaScanner.Scan(resource);

            return resource;
        }

        public MediaResource CommitResource(string uri)
        {
            var resource = ScanResource(uri);
            var scanner = ResourceScannerFactory.GetHandler(resource.Type);

            if (scanner != null)
            {
                scanner.Commit(resource);
            }

            return resource;
        }
    }
}