using System.Web.Optimization;

namespace PirateJobBoard.UI.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/preCDN").Include(
                      "~/Content/vendors/popperjs/popper.min.js",
                      "~/Content/vendors/bootstrap/bootstrap.min.js",
                      "~/Content/vendors/is/is.min.js",
                      "~/Content/vendors/fontawesome/all.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/postCDN").Include(
                      "~/Content/assets/js/theme.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/assets/css/theme.min.css",
                      "~/Content/assets/css/custom.min.css"));

            
        }
    }
}
