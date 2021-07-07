using System.Web.Optimization;

namespace Clicks.Yoga.Portal
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/js/head")
                .Include("~/Scripts/libs/jquery-1.9.1.min.js")
                .Include("~/Scripts/libs/modernizr-2.5.3.min.js")
                .IncludeDirectory("~/Scripts/features", "*.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/body")
                .Include("~/Scripts/libs/jquery-ui/js/jquery-ui-1.10.3.custom.min.js")
                .Include("~/Scripts/libs/jquery.cookie.js")
                .Include("~/Scripts/libs/jquery.easing.1.3.js")
                .Include("~/Scripts/libs/jquery.jscrollpane.min.js")
                .Include("~/Scripts/libs/jquery.mousewheel.js")
                .Include("~/Scripts/libs/jquery.stellar.min.js")
                .Include("~/Scripts/libs/hogan-2.0.0.min.js")
                .Include("~/Scripts/libs/markerclusterer.js")
                .Include("~/Scripts/libs/moment/moment.js")
                .Include("~/Scripts/libs/mwheelIntent.js")
                .Include("~/Scripts/libs/underscore-min.js")
                .Include("~/Scripts/libs/waypoints.min.js")
                .Include("~/Scripts/libs/fancyBox/jquery.fancybox.js")
                .Include("~/Scripts/libs/plupload/plupload.full.js")
                .Include("~/Scripts/libs/jcrop/jquery.Jcrop.min.js")
                .Include("~/Scripts/libs/jcrop/ProfileImagesCrop.js")
                .Include("~/Scripts/readmore.js")
                .Include("~/Scripts/trainingorganisations.js")
                .Include("~/Scripts/highlightnav.js")
                .Include("~/scripts/owl.carousel.js")
                .Include("~/scripts/plugins.js")
                .Include("~/scripts/tabmenu.js")
                .Include("~/scripts/Placeholders.min.js")
                .Include("~/scripts/owl.carousel.js"));

            bundles.Add(new StyleBundle("~/bundles/css")
                .Include("~/css/less/css/h5bp-reset.css", new CssRewriteUrlTransform())
                .Include("~/css/less/css/new_style.css", new CssRewriteUrlTransform())
                .Include("~/css/less/owl.carousel.css", new CssRewriteUrlTransform())
                .Include("~/css/less/owl.theme.css", new CssRewriteUrlTransform())
                .Include("~/Scripts/libs/jquery-ui/css/smoothness/jquery-ui-1.10.3.custom.min.css", new CssRewriteUrlTransform())
                .Include("~/Scripts/libs/fancyBox/jquery.fancybox.css", new CssRewriteUrlTransform())
                .Include("~/css/jquery.Jcrop.min.css", new CssRewriteUrlTransform()));
        }
    }
}