using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common
{
    public static class StringExtensions
    {
        public static string ParseUris(this string input)
        {
            return Regex.Replace(input, @"((http|https):\/\/)?([\w\-_]+(?:(?:\.[\w\-_]+)+))([\w\-\.,@?^=%&amp;:/~\+#]*[\w\-\@?^=%&amp;/~\+#])?",
                m =>
                {
                    if (string.IsNullOrEmpty(m.Groups[1].Value) && (string.IsNullOrEmpty(m.Groups[3].Value) || !m.Groups[3].Value.StartsWith("www")))
                    {
                        return m.Groups[0].Value;
                    }
                    return string.Format("<a href=\"{0}\" target=\"_blank\">{1}</a>", string.IsNullOrEmpty(m.Groups[1].Value) ? "http://" + m.Groups[0].Value : m.Groups[0].Value, m.Groups[3].Value);
                },
                RegexOptions.Compiled);
        }
    }
}
