using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using Clicks.Yoga.Domain.Entities;
using HtmlAgilityPack;

namespace Clicks.Yoga.Media.Scanners
{
    public abstract class WebMediaScanner : IMediaResourceScanner
    {
        protected const string UserAgent = "YogaClicksBot/1.0 (bot@yogaclicks.com http://yogaclicks.com/)";

        protected MediaResource Resource { get; private set; }

        protected HtmlDocument Document { get; private set; }

        public void Scan(MediaResource resource)
        {
            if (resource == null)
                throw new ArgumentNullException("resource");

            Uri uri;

            if (
                !Uri.TryCreate(resource.Uri, UriKind.Absolute, out uri) ||
                (uri.Scheme != "http" && uri.Scheme != "https"))
            {
                throw new UnsupportedMediaException("Resource must have a valid HTTP or HTTPS URI.");
            }

            Resource = resource;
            Document = GetDocument(resource.Uri);

            if (Document != null)
            {
                ScanDocument();
            }
            else
            {
                throw new UnsupportedMediaException();
            }
        }

        public void Commit(MediaResource resource) { }

        protected abstract void ScanDocument();

        protected virtual HtmlDocument GetDocument(string url)
        {
            var document = new HtmlDocument();

            using (var client = new WebClient())
            {
                client.Headers.Add("User-Agent", UserAgent);

                try
                {
                    using (var stream = client.OpenRead(url))
                    {
                        try
                        {
                            document.Load(stream, Encoding.UTF8);
                        }
                        catch (Exception)
                        {
                            document = null;
                        }
                    }
                }
                catch (WebException)
                {
                    document = null;
                }
            }

            return document;
        }

        protected string GetOpenGraphProperty(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (!Regex.IsMatch(name, "^og:[a-z:-]+$"))
                throw new ArgumentException("Name must be a valid OpenGraph property.", "name");

            var path = string.Format("//meta[@property='{0}']", name);
            return GetAttributeValue(path, "content");
        }

        protected string GetMetaProperty(string name)
        {
            if (name == null)
                throw new ArgumentNullException("name");
            if (!Regex.IsMatch(name, "^[a-z]+"))
                throw new ArgumentException("Name must be a valid HTML meta tag name value.", "name");

            var path = string.Format("//meta[@name='{0}']", name);
            return GetAttributeValue(path, "content");
        }

        protected string GetAttributeValue(string path, string name)
        {
            if (name == null)
                throw new ArgumentNullException("path");
            if (name == null)
                throw new ArgumentNullException("name");

            var node = Document.DocumentNode.SelectSingleNode(path);
            return node != null ? HtmlEntity.DeEntitize(node.GetAttributeValue(name, null)) : null;
        }

        protected string GetElementValue(string path)
        {
            if (path == null)
                throw new ArgumentNullException("path");

            var node = Document.DocumentNode.SelectSingleNode(path);
            return node != null ? HtmlEntity.DeEntitize(node.InnerText) : null;
        }
    }
}