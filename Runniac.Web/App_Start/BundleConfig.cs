using System.Web;
using System.Web.Optimization;

namespace Runniac.Web
{
    public class BundleConfig
    {
        // Para obtener más información acerca de Bundling, consulte http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                "~/Scripts/modernizr-*",
                "~/Scripts/jquery-1.9.1.min.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/jquery.blueimp-gallery.min.js",
                "~/Scripts/bootstrap-image-gallery.js",
                "~/Scripts/angular-file-upload-shim.js",
                "~/Scripts/angular.min.js",
                "~/Scripts/angular-file-upload.js",
                "~/Scripts/angular-route.min.js",
                "~/Scripts/angular-resource.min.js",
                "~/Scripts/angular-sanitize.min.js",
                "~/Scripts/angular-tablesort.js",
                "~/Scripts/angular-css.min.js",
                "~/Scripts/angularjs-geolocation.min.js",
                "~/Scripts/toastr.js",
                "~/Scripts/moment.js",
                "~/Scripts/datetimepicker.js",
                "~/Scripts/i18n/angular-locale_es-es.js",
                "~/Scripts/ui-bootstrap-tpls-0.11.0.js",
                "~/Scripts/ng-grid-2.0.11.min.js",
                "~/Scripts/ng-grid-flexible-height.js",
                "~/Scripts/lodash.underscore.min.js", 
                "~/Scripts/ol.js",    
                "~/Scripts/angular-openlayers-directive.js",
                "~/Scripts/jquery.easing.min.js",
                "~/Scripts/grayscale.js",
                "~/Scripts/App/app.js",
                "~/Scripts/App/routing.js",
                "~/Scripts/App/directives.js",
                "~/Scripts/App/customFilters.js",
                "~/Scripts/App/factories.js",
                "~/Scripts/App/utils/mapUtils.js",
                "~/Scripts/App/utils/conversionUtils.js",
                "~/Scripts/App/utils/browserUtils.js",   
                "~/Scripts/App/services/anchor-smooth-scroll.js",
                "~/Scripts/App/controllers/AppCtrl.js",
                "~/Scripts/App/controllers/LandingCtrl.js",
                "~/Scripts/App/controllers/events/EventsCtrl.js",
                "~/Scripts/App/controllers/events/SearchEventsCtrl.js",
                "~/Scripts/App/controllers/events/EventDetailCtrl.js",
                "~/Scripts/App/controllers/comments/CreateCommentCtrl.js",
                "~/Scripts/App/controllers/photos/PhotoUploaderCtrl.js",
                "~/Scripts/App/controllers/votes/VotesCtrl.js",
                "~/Scripts/App/controllers/events/SuggestEventCtrl.js",
                "~/Scripts/App/controllers/events/ShowMoreInfoCtrl.js",
                "~/Scripts/App/controllers/events/PreviewCourseCtrl.js",
                "~/Scripts/App/controllers/events/ExpandMapCtrl.js",
                "~/Scripts/App/controllers/common/ConfirmationCtrl.js",
                "~/Scripts/App/controllers/users/UsersCtrl.js"                
                ));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/Site.css",
                "~/Content/tablesort.css",
                "~/Content/toastr.css",
                "~/Content/datetimepicker.css",
                "~/Content/ng-grid.min.css",
                "~/Content/blueimp-gallery.min.css",
                "~/Content/bootstrap-image-gallery.css",
                "~/Content/ol.css",
                "~/Content/angular-openlayers-directive.css",                
                "~/Content/google-fonts.css",
                "~/Content/font-awesome.min.css"));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                       "~/Scripts/jquery.unobtrusive*",
                       "~/Scripts/jquery.validate*"));

        }
    }
}