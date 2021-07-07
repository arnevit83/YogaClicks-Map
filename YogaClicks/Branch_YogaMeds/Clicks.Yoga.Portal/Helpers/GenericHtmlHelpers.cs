using System;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Common;

namespace Clicks.Yoga.Portal.Helpers
{
    public static class GenericHtmlHelpers
    {
        private static readonly Regex NewLineRegex = new Regex("\\r\\n|\\r|\\n");

        public static MvcHtmlString NewLines(this HtmlHelper helper, string text)
        {
            if (string.IsNullOrEmpty(text))
                return new MvcHtmlString(string.Empty);

            return new MvcHtmlString(NewLineRegex.Replace(HttpUtility.HtmlEncode(text), "<br/>"));
        }

        public static MvcHtmlString NewLinesAndUris(this HtmlHelper helper, string text)
        {
            if (string.IsNullOrEmpty(text))
                return new MvcHtmlString(string.Empty);

            var str = NewLineRegex.Replace(HttpUtility.HtmlEncode(text), "<br/>");

            return new MvcHtmlString(str.ParseUris());
        }

        public static MvcHtmlString MultiLine(this HtmlHelper helper, string text)
        {
            if (string.IsNullOrEmpty(text))
                return new MvcHtmlString(string.Empty);

            return new MvcHtmlString(text.Replace(", ", "<br/>"));
        }

        public static MvcHtmlString Trim(this HtmlHelper heler, string text, int characterCount)
        {
            if (string.IsNullOrEmpty(text))
                return new MvcHtmlString(string.Empty);

            if (text.Length <= characterCount)
                return new MvcHtmlString(text);

            return new MvcHtmlString(text.Substring(0, characterCount) + "...");
        }

        public static MvcHtmlString StarRatingClass(this HtmlHelper helper, decimal rating)
        {
            return StarRatingClass(helper, (int)Math.Ceiling(rating * 2));
        }

        public static MvcHtmlString StarRatingClass(this HtmlHelper helper, int rating)
        {
            var str = string.Empty;

            switch (rating)
            {
                case 1:
                    str = "half";
                    break;
                case 2:
                    str = "one";
                    break;
                case 3:
                    str = "one-half";
                    break;
                case 4:
                    str = "two";
                    break;
                case 5:
                    str = "two-half";
                    break;
                case 6:
                    str = "three";
                    break;
                case 7:
                    str = "three-half";
                    break;
                case 8:
                    str = "four";
                    break;
                case 9:
                    str = "four-half";
                    break;
                case 10:
                    str = "five";
                    break;
            }

            return new MvcHtmlString(str);
        }

        public static MvcHtmlString HiddenOptional(this HtmlHelper helper, string name, object value)
        {
            if (value != null) return helper.Hidden(name, value);
            return new MvcHtmlString("");
        }

        public static MvcHtmlString HiddenOptional(this HtmlHelper helper, string name, string value)
        {
            if (!string.IsNullOrEmpty(value)) return helper.Hidden(name, value);
            return new MvcHtmlString("");
        }

        public static MvcHtmlString LocalDateTime(this HtmlHelper helper, string dateIn)
        {
            var tag = new TagBuilder("span");
            tag.AddCssClass("localDateTime");
            tag.SetInnerText(dateIn);
            return MvcHtmlString.Create(tag.ToString(TagRenderMode.Normal));
        }
    }
}