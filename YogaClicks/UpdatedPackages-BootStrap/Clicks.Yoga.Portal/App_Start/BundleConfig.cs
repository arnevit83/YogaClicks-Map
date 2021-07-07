using System.Web.Optimization;

namespace Clicks.Yoga.Portal
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {

            bundles.UseCdn = true;   //enable CDN support
            BundleTable.EnableOptimizations = true;

            bundles.Add(new ScriptBundle("~/bundles/js/head")
                .Include("~/Scripts/libs/jquery-1.9.1.min.js")
                .Include("~/Scripts/libs/modernizr-2.5.3.min.js")
                .Include("~/Scripts/bootstrap.js")
                .IncludeDirectory("~/Scripts/features", "*.js"));





            bundles.Add(new ScriptBundle("~/bundles/js/body")
                .Include("~/Scripts/libs/jquery-ui/js/jquery-ui-1.10.3.custom.min.js")
                .Include("~/Scripts/libs/jquery.cookie.js")
                .Include("~/Scripts/libs/jquery.easing.1.3.js")
               // .Include("~/Scripts/libs/jquery.jscrollpane.min.js")
              //  .Include("~/Scripts/libs/jquery.mousewheel.js")
                .Include("~/Scripts/libs/jquery.stellar.min.js")
                .Include("~/Scripts/libs/hogan-2.0.0.min.js")
                .Include("~/Scripts/libs/markerclusterer.js")  //!Should be displayed only on map page,Leaving because I am refactoring later
                .Include("~/Scripts/libs/moment/moment.js")  //Global Dates
                .Include("~/Scripts/libs/mwheelIntent.js")  //Scroll bars
                .Include("~/Scripts/libs/underscore-min.js")  //Not sure where its used yet
                // .Include("~/Scripts/libs/waypoints.min.js") // Do things happen on scroll position
                .Include("~/Scripts/libs/fancyBox/jquery.fancybox.js") //boot strap light box
                .Include("~/Scripts/libs/plupload/plupload.full.js")  //Video Uploader
                .Include("~/Scripts/libs/jcrop/jquery.Jcrop.min.js")  //image cropping
                .Include("~/Scripts/libs/jcrop/ProfileImagesCrop.js")  //image cropping settings
                .Include("~/Scripts/readmore.js")   //Readmore Expandable objects
                // .Include("~/Scripts/trainingorganisations.js")
                .Include("~/Scripts/highlightnav.js") //!Wont need in Bootstrap
                .Include("~/scripts/owl.carousel.js")
                .Include("~/scripts/plugins.js") //Console write
                .Include("~/scripts/tabmenu.js")  //!Affects bootstrap warning
                .Include("~/scripts/Placeholders.min.js") //! can be removed with bootstrap
                //.Include("~/scripts/owl.carousel.js")
                .Include("~/scripts/script.js")
                .Include("~/Scripts/buttons.js")
                .Include("~/Scripts/PreScript.js")
                .Include("~/Scripts/Pingdom.js"));


            //Maps
            const string mapsjsapi = "http://www.google.com/jsapi?sensor=false";
            bundles.Add(new ScriptBundle("~/bundles/mapsapi", mapsjsapi));
            
            const string maps = "http://maps.googleapis.com/maps/api/js";
            bundles.Add(new ScriptBundle("~/bundles/maps", maps));
            //End of Maps

            bundles.Add(new StyleBundle("~/bundles/css")
                .Include("~/Content/bootstrap.css")
                .Include("~/Content/font-awesome.css")
                .IncludeDirectory("~/Content/css/layout", "*.css")
                .Include("~/css/less/css/h5bp-reset.css")
                .Include("~/css/less/css/new_style.css")
                .Include("~/Scripts/libs/jquery-ui/css/smoothness/jquery-ui-1.10.3.custom.css")
                .Include("~/Scripts/libs/fancyBox/jquery.fancybox.css")
                .Include("~/css/jquery.Jcrop.css")
                .Include("~/css/less/owl.carousel.css")
                .Include("~/css/less/owl.theme.css")
                );  //image cropping
            
            

        }
    }
}