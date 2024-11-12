using System.Web;
using System.Web.Optimization;

namespace waats
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

            bundles.Add(new ScriptBundle("~/bundles/jqueryUnob").Include(
                        "~/Scripts/jquery.validate.unobtrusive.js"));

            bundles.Add(new ScriptBundle("~/bundles/dataTables").Include(
                     "~/Scripts/DataTables/jquery.dataTables.js",
                     "~/Scripts/DataTables/dataTables.bootstrap4.js",
                     "~/Scripts/DataTables/dataTables.rowReorder.js"));//,
            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/moment.js",
                      "~/Scripts/app.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.css",
                      "~/Content/css/style.css",
                      "~/Content/css/dashboard.css",
                      "~/Content/css/responsive.css"));
            bundles.Add(new StyleBundle("~/Content/datatablescss").Include(
                       "~/Content/DataTables/css/dataTables.bootstrap4.css",
                       "~/Content/DataTables/css/jquery.dataTables.css",
                       "~/Content/DataTables/css/rowReorder.dataTables.css",
                       "~/Content/DataTables/css/fixedHeader.bootstrap4.css"));

            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
           "~/Scripts/toastr.js"));

            bundles.Add(new StyleBundle("~/Content/toastrcss").Include(
            "~/Content/toastr.css"));
        }
    }
}
