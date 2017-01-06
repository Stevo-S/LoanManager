using System.Web;
using System.Web.Optimization;

namespace LoanManager
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

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            
            // Bundle Grid.Mvc scripts and stylesheets
            bundles.Add(new ScriptBundle("~/bundles/gridmvc").Include("~/Scripts/gridmvc.min.js"));
            bundles.Add(new StyleBundle("~/Content/gridmvc").Include("~/Content/Gridmvc.css"));

            // Include Font-Awesome
            bundles.Add(new StyleBundle("~/Content/font_awesome").Include("~/Content/font-awesome.css"));

            // custom stylesheet
            bundles.Add(new StyleBundle("~/Content/customcss").Include("~/Content/custom.css"));

            // Bundle and minify custom scripts
            bundles.Add(new ScriptBundle("~/bundles/custom").Include(
                "~/Scripts/loans.js",
                "~/Scripts/transactions.js"));
        }
    }
}
