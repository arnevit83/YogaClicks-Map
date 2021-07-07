using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Domain.Services;

namespace Clicks.Yoga.Media.Scanners
{
    public class ImageWebMediaScanner : WebMediaScanner
    {
        private const int RequestTimeout = 2000;
        private const int MinimumSize = 4096;
        private const int MinimumWidth = 100;
        private const int MinimumHeight = 25;
        private const int MaximumRatio = 4;

        private static readonly PropertyInfo MediaResourceImageProperty = typeof(MediaResource).GetProperty("Image");

        public ImageWebMediaScanner(
            IImageService imageService)
        {
            ImageService = imageService;
        }

        private IImageService ImageService { get; set; }

        protected override void ScanDocument()
        {
            var url = GetOpenGraphProperty("og:image");
            Image image = null;

            if (url != null)
            {
                image = GetAcceptableImage(url);
            }

            if (image == null)
            {
                var uri = new Uri(Resource.Uri);

                foreach (var candidate in GetCandidates(uri))
                {
                    image = GetAcceptableImage(candidate.Url);

                    if (image != null) break;
                }
            }

            Resource.Image = image ?? Resource.Image;
        }

        private IEnumerable<Candidate> GetCandidates(Uri baseUri)
        {
            var candidates = new List<Candidate>();

            var nodes = Document.DocumentNode.SelectNodes("//img");

            if (nodes == null)
            {
                return candidates;
            }

            foreach (var node in nodes)
            {
                var src = node.GetAttributeValue("src", null);

                if (src == null) continue;

                try
                {
                    var uri = new Uri(baseUri, src);
                    var width = node.GetAttributeValue("width", 0);
                    var height = node.GetAttributeValue("height", 0);

                    if (width > 0 && height > 0 && !DimensionsAcceptable(width, height))
                    {
                        continue;
                    }

                    var candidate = new Candidate
                    {
                        Url = uri.ToString(),
                        Width = width,
                        Height = height
                    };

                    candidates.Add(candidate);
                }
                catch (FormatException) { }
            }

            return
                candidates
                    .Where(c => c.Width == 0 || c.Width >= MinimumWidth)
                    .Where(c => c.Height == 0 || c.Height >= MinimumHeight)
                    .OrderByDescending(c => c.Width + c.Height)
                    .ToList();
        }

        public Image GetAcceptableImage(string url)
        {
            if (url == null)
                throw new ArgumentNullException("url");

            Image image = null;
            System.Drawing.Image source = null;

            var request = WebRequest.Create(url) as HttpWebRequest;

            if (request == null)
            {
                return null;
            }

            request.Method = "GET";
            request.UserAgent = UserAgent;
            request.Timeout = RequestTimeout;

            try
            {
                using (var response = request.GetResponse())
                {
                    var contentLength = response.Headers["Content-Length"];
                    int size;

                    if (!int.TryParse(contentLength, out size) || size < MinimumSize)
                    {
                        return null;
                    }

                    using (var stream = response.GetResponseStream())
                    {
                        if (stream == null)
                        {
                            return null;
                        }

                        try
                        {
                            source = System.Drawing.Image.FromStream(stream);
                        }
                        catch (ArgumentException)
                        {
                            return null;
                        }
                    }
                }

                if (!DimensionsAcceptable(source.Width, source.Height))
                {
                    return null;
                }

                image = ImageService.CreateImage(MediaResourceImageProperty, source);
            }
            catch (WebException) { }
            catch (OutOfMemoryException) { }
            finally
            {
                if (source != null) source.Dispose();
            }

            return image;
        }

        private bool DimensionsAcceptable(int width, int height)
        {
            if (width == 0 || height == 0)
            {
                return false;
            }

            var ratio = (decimal)width / height;
            ratio = ratio < 1 ? 1 / ratio : ratio;

            return
                width >= MinimumWidth &&
                height >= MinimumHeight &&
                ratio <= MaximumRatio;
        }

        private class Candidate
        {
            public string Url { get; set; }

            public int Width { get; set; }

            public int Height { get; set; }
        }
    }
}