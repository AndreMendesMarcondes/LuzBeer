using System.Web;
using System.Web.Optimization;

namespace LuzBeer
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/TemplateCss").Include(
                      "~/css/bootstrap.min.css",
                      "~/css/fakeLoader.css",
                      "~/css/ie9.css",
                      "~/css/owl-carousel.min.css",
                      "~/style.css"));

            bundles.Add(new ScriptBundle("~/bundles/TemplateJS").Include(
                   "~/js/jquery-2.2.4.min.js",
                    "~/js/jquery-ui.min.js",
                    "~/js/jquery.js",
                    "~/js/bootstrap.min.js",
                    "~/js/counterup.js",
                    "~/js/custom.js",
                    "~/js/fakeLoader.min.js",
                    "~/js/gmap3.js",
                    "~/js/gmap3.min.js",
                    "~/js/headhesive.min.js",                 
                    "~/js/matchHeight.min.js",
                    "~/js/modernizr.custom.js",
                    "~/js/npm.js",
                    "~/js/owl.autoplay.js",
                    "~/js/owl.carousel.js",
                    "~/js/scrollme.min.js",
                    "~/js/waypoints.min.js"));
        }
    }
}
