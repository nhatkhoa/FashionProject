using System.Web.Optimization;

namespace IdentitySample
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

           

            bundles.Add(new StyleBundle("~/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/style/font-awesome.min.css",
                      "~/Content/style/camera.css",
                      "~/Content/style/owl.carousel.css",
                      "~/Content/style/owl.transitions.css",
                      "~/Content/style/jquery.custom-scrollbar.css",
                      "~/Content/style/style.css"));

            bundles.Add(new ScriptBundle("~/js").Include(
                        "~/Content/js/jquery-2.1.0.min.js",
                        "~/Content/js/jquery-migrate-1.2.1.min.js",
                        "~/Content/js/retina.js",
                        "~/Content/js/camera.min.js",
                        "~/Content/js/jquery.easing.1.3.js",
                        "~/Content/js/waypoints.min.js",
                        "~/Content/js/jquery.isotope.min.js",
                        "~/Content/js/owl.carousel.min.js",
                        "~/Content/js/jquery.tweet.min.js",
                        "~/Content/js/jquery.custom-scrollbar.js",
                        "~/Content/js/scripts.js"));
            
            bundles.Add(new ScriptBundle("~/app").Include(
                        "~/Scripts/angular.min.js",
                        "~/Scripts/ngStorage.min.js",
                        "~/Scripts/angular-filter.min.js",
                        "~/Content/app.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                     "~/Content/bootstrap.min.css",
                     "~/Content/site.css"));
            // --- Nén dữ liệu
            System.Web.Optimization.BundleTable.EnableOptimizations = false;
        }
    }
}
