using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace Clicks.Yoga.Web
{
    public class WebUtility
    {
        public const string UrlListSeparator = "~";

        public static string UrlSlug(string text)
        {
            return Regex.Replace(text.ToLower().Trim(), "[^a-z0-9-]+", "-");
        }

        public static string UrlSlug<T>(IEnumerable<T> list)
        {
            return string.Join(UrlListSeparator, list.Select(i => i.ToString()));
        }

        public static List<T> SplitUrlSlug<T>(string slug)
        {
            var result = new List<T>();

            if (string.IsNullOrEmpty(slug)) return result;

            foreach (var part in slug.Split(new string[] { UrlListSeparator }, StringSplitOptions.None))
            {
                try
                {
                    result.Add((T)Convert.ChangeType(part, typeof(T)));
                }
                catch (Exception) {}
            }

            return result;
        }

        public static string SanitiseLocalUri(string uri, Uri baseUri)
        {
            if (uri == null) throw new ArgumentNullException("uri");

            try
            {
                var url = new Uri(baseUri, uri);
                uri = Regex.Replace(url.PathAndQuery, "\\?$", "");
                return uri;
            }
            catch (UriFormatException)
            {
                return "/";
            }
        }

        public static string NormaliseHttpUrl(string uri)
        {
            if (uri == null) throw new ArgumentNullException("uri");

            if (!uri.Contains("://")) uri = "http://" + uri;
            return uri.ToLower();
        }

        public static bool IsValidHttpUrl(string url)
        {
            Uri uri;

            if (Uri.TryCreate(url, UriKind.Absolute, out uri))
            {
                return uri.Scheme.StartsWith("http") && Regex.IsMatch(uri.Host, @"^[a-z0-9]+([.-][a-z0-9]+)*\.[a-z]{2,6}$");
            }
            
            return false;
        }
    }
}