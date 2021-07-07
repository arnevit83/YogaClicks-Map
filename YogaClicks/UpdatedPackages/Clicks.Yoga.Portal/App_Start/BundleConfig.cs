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
                //.Include("~/Scripts/libs/jquery.stellar.min.js")
                .Include("~/Scripts/libs/hogan-2.0.0.min.js")
                .Include("~/Scripts/libs/markerclusterer.js")  //!Should be displayed only on map page,Leaving because I am refactoring later
                .Include("~/Scripts/libs/moment/moment.js")  //Global Dates
                .Include("~/Scripts/libs/mwheelIntent.js")  //Scroll bars
                .Include("~/Scripts/libs/underscore-min.js")
                  
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
                //.Include("~/Scripts/buttons.js")
                .Include("~/Scripts/PreScript.js")
                //.Include("~/Scripts/Pingdom.js")
                .Include("~/Scripts/jQuery.isValid.js")
                .Include("~/Scripts/libs/chosen/chosen.jquery.js")
                );

            bundles.Add(new ScriptBundle("~/bundles/js/Clear")
           .IncludeDirectory("~/Scripts/features", "*.js")
           .Include("~/Scripts/libs/jquery.cookie.js")
           .Include("~/Scripts/libs/jquery.easing.1.3.js")
           .Include("~/Scripts/libs/fancyBox/jquery.fancybox.js") 
           .Include("~/scripts/owl.carousel.js")
           .Include("~/scripts/script.js")
             .Include("~/Scripts/libs/Counterup/jquery.counterup.min.js")//countup
           //.Include("~/Scripts/buttons.js")
           .Include("~/Scripts/readmore.js")
           .Include("~/Scripts/PreScript.js")
    
           );

            //  HomepageC:\Duncan Source\YogaClicks\UpdatedPackages\Clicks.Yoga.Portal\Content\css\Homepage\home.css
            bundles.Add(new StyleBundle("~/bundles/Clear")
               
               .Include("~/Content/bootstrap.min.css")
                .Include("~/Content/font-awesome.min.css")
                .IncludeDirectory("~/Content/css/layout", "*.css")
                .Include("~/css/less/css/h5bp-reset.min.css")
                .Include("~/css/less/css/new_style.min.css")
                .Include("~/Scripts/libs/jquery-ui/css/smoothness/jquery-ui-1.10.3.custom.min.css")
                .Include("~/Scripts/libs/fancyBox/jquery.fancybox.min.css")
                .Include("~/css/jquery.Jcrop.min.css")
                .Include("~/css/less/owl.carousel.min.css")
                .Include("~/css/less/owl.theme.min.css")
                .Include("~/css/less/css/ps.min.css")
                .Include("~/CSS/chosen.min.css")
                .IncludeDirectory("~/Content/css/signup", "*.css")


                   .Include("~/Content/css/activities/activities.min.css")
                   .Include("~/Content/css/CreateCourse.min.css")
                       .Include("~/Content/css/Homepage/home.min.css")
            );


            //Maps
            const string mapsjsapi = "http://www.google.com/jsapi?sensor=false";
            bundles.Add(new ScriptBundle("~/bundles/mapsapi", mapsjsapi));
            
            const string maps = "http://maps.googleapis.com/maps/api/js";
            bundles.Add(new ScriptBundle("~/bundles/maps", maps));
            //End of Maps

            bundles.Add(new StyleBundle("~/bundles/css") 
                .Include("~/Content/bootstrap.min.css")
                .Include("~/Content/font-awesome.min.css")
                .IncludeDirectory("~/Content/css/layout", "*.css")
                .Include("~/css/less/css/h5bp-reset.min.css")
                .Include("~/css/less/css/new_style.min.css")
                .Include("~/Scripts/libs/jquery-ui/css/smoothness/jquery-ui-1.10.3.custom.min.css")
                .Include("~/Scripts/libs/fancyBox/jquery.fancybox.min.css")
                .Include("~/css/jquery.Jcrop.min.css")
                .Include("~/css/less/owl.carousel.min.css")
                .Include("~/css/less/owl.theme.min.css")
                .Include("~/css/less/css/ps.min.css")
                .Include("~/CSS/chosen.min.css")
                .IncludeDirectory("~/Content/css/signup", "*.css")
             
                
                   .Include("~/Content/css/activities/activities.min.css")
                   .Include("~/Content/css/CreateCourse.min.css")
                );  //image cropping

            bundles.Add(new StyleBundle("~/bundles/signupcss")
                .Include("~/Content/bootstrap.min.css")
                .Include("~/Content/font-awesome.min.css")
                .Include("~/Scripts/libs/jquery-ui/css/smoothness/jquery-ui-1.10.3.custom.min.css")
                .Include("~/Scripts/libs/fancyBox/jquery.fancybox.min.css")
                .Include("~/css/jquery.Jcrop.min.css")
                .Include("~/css/less/owl.carousel.min.css")
                .Include("~/css/less/owl.theme.min.css")
                .Include("~/CSS/chosen.min.css")
                .IncludeDirectory("~/Content/css/signup", "*.css")
                );  //image cropping

           
            bundles.Add(new StyleBundle("~/bundles/Validation")

             .Include("~/Scripts/jquery.validate.min.js")
                 .Include("~/Scripts/jquery.validate.unobtrusive.min.js")
               .Include("~/Scripts/jquery.validate.unobtrusive.bootstrap.min.js")
          );


            
            //Non partial includes

            //YogaMap
            bundles.Add(new StyleBundle("~/bundles/StoriesMap")
                .Include("~/CSS/chosen.min.css")
              .IncludeDirectory("~/Content/css/Stories", "*.css")
          
               
            );
          
            
            bundles.Add(new ScriptBundle("~/bundles/js/StoriesMap")
            .IncludeDirectory("~/Scripts/Stories", "*.js")
                .Include("~/Scripts/libs/jcrop/ProfileImagesCrop.js")
                  .Include("~/Scripts/StoriesPopup/StorysPopup.min.js")
                .Include("~/Scripts/libs/chosen/chosen.jquery.min.js")
                .Include("~/Scripts/libs/jcrop/jquery.Jcrop.min.js")  //image cropping
                .Include("~/Scripts/libs/jcrop/ProfileImagesCrop.js")
             
             );

         

        }
    }
}