using System;
using System.Text.RegularExpressions;

namespace Clicks.Yoga.Domain.Entities
{
    public class VideoType : Entity
    {
        public string Name { get; set; }

        public string UrlPattern { get; set; }

        public string EmbedHtml { get; set; }

        public bool TryParseUrl(string url, out string identifier)
        {
            var match = Regex.Match(url, UrlPattern, RegexOptions.IgnoreCase);
            
            if (match.Success)
            {
                if (match.Groups.Count < 1)
                {
                    throw new Exception("URL pattern must have at least one capturing group");
                }

                identifier = match.Groups[1].Value;

                return true;
            }

            identifier = null;

            return false;
        }
    }
}
