using System.Web;
using System.Web.Optimization;

namespace Legalacts.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-1.11.1.min.js",
                        "~/Scripts/jquery-ui.min.js",
                        "~/Scripts/jquery.easing.1.3.js"));

            bundles.Add(new ScriptBundle("~/bundles/select2").Include(
                      "~/Scripts/select2.min.js",
                      "~/Scripts/select2_locale_bg.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                      "~/Scripts/scripts.js"));

            bundles.Add(new ScriptBundle("~/bundles/canvasloader").Include(
                      "~/Scripts/canvasloader-min-0.9.1.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/styles/bootstrap.min.css",
                      "~/Content/styles/jquery-ui.min.css",
                      "~/Content/styles/select2.css",
                      "~/Content/styles/style.css"));

            // Set EnableOptimizations to false for debugging. For more information,
            // visit http://go.microsoft.com/fwlink/?LinkId=301862
            //BundleTable.EnableOptimizations = true;
        }
    }
}
