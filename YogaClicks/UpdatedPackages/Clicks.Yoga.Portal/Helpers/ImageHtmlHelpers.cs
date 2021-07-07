using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Portal.Models.Entities;
using ImageResizer.FluentExtensions;
using ImageResizer.FluentExtensions.Mvc;

namespace Clicks.Yoga.Portal.Helpers
{
    public static class ImageHtmlHelpers
    {
        public static MvcHtmlString Image(this HtmlHelper helper, Image image, Action<ImageUrlBuilder> configure, string alternateText = null, object htmlAttributes = null)
        {
            return helper.Image(image == null ? null : image.Uri, null, configure, alternateText, htmlAttributes);
        }

        public static MvcHtmlString Image(this HtmlHelper helper, Image image, string defaultName, Action<ImageUrlBuilder> configure, string alternateText = null, object htmlAttributes = null)
        {
            return helper.Image(image == null ? null : image.Uri, defaultName, configure, alternateText, htmlAttributes);
        }
        public static MvcHtmlString Image(this HtmlHelper helper, ImageModel image, Action<ImageUrlBuilder> configure, string alternateText = null, object htmlAttributes = null)
        {
            return helper.Image(image == null || !image.Exists ? null : image.Uri, null, configure, alternateText, htmlAttributes);
        }

        public static MvcHtmlString Image(this HtmlHelper helper, ImageModel image, string defaultName, Action<ImageUrlBuilder> configure, string alternateText = null, object htmlAttributes = null)
        {
            return helper.Image(image == null || !image.Exists ? null : image.Uri, defaultName, configure, alternateText, htmlAttributes);
        }

        public static MvcHtmlString ImageUrl(this HtmlHelper helper, ImageModel image)
        {
            return ImageUrl(helper, image, false);
        }

        public static MvcHtmlString ImageUrl(this HtmlHelper helper, ImageModel image, bool encode)
        {
            var url = image.Url;

            if (encode) return new MvcHtmlString(helper.Encode(url));

            return new MvcHtmlString(url);
        }

        private static MvcHtmlString Image(this HtmlHelper helper, string name, string defaultName, Action<ImageUrlBuilder> configure, string alternateText, object htmlAttributes = null)
        {

            var builder = new ImageUrlBuilder();
            if (configure != null) configure(builder);
            return helper.Image(name, defaultName, builder, alternateText, htmlAttributes);
        }

        private static MvcHtmlString Image(this HtmlHelper helper, string name, string defaultName, ImageUrlBuilder builder, string alternateText, object htmlAttributes = null)
        {
            if (name == null && defaultName == null) return new MvcHtmlString("");

            var baseUri = new Uri(ConfigurationManager.AppSettings["Clicks.Yoga.ImageStore.Url"]);

            baseUri = new Uri(baseUri, name != null ? "images/yogaimages/" : "defaults/");
            baseUri = new Uri(baseUri, name ?? defaultName);
            
            var resizedUri = new Uri(new UrlHelper(helper.ViewContext.RequestContext, helper.RouteCollection).ImageUrl(baseUri.ToString(), builder));

            var src = resizedUri.ToString();

            if (src.StartsWith("~/")) src = VirtualPathUtility.ToAbsolute(src);

            var tagBuilder = new TagBuilder("img");

            tagBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));
            tagBuilder.MergeAttribute("src", src);
            tagBuilder.MergeAttribute("alt", alternateText);
            tagBuilder.MergeAttribute("data-uri", baseUri.ToString());
            tagBuilder.MergeAttribute("data-query", resizedUri.Query);

            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.SelfClosing));
        }
    }
}