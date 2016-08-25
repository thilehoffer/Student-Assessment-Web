using System.Web;
using System.Web.Optimization;

namespace AssessmentApp.WebClient
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
             
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/all")
                 .Include("~/Scripts/modernizr-2.6.2.js")
                 .Include("~/Scripts/jquery-1.10.2.min.js") 
                 .Include("~/Scripts/jquery.validate.js")
                 .Include("~/Scripts/bootstrap.js")
                 .Include("~/Scripts/respond.js")
                 .Include("~/Scripts/kendo/2016.1.112/jquery.min.js")
                 .Include("~/Scripts/kendo/2016.1.112/jszip.min.js")
                 .Include("~/Scripts/kendo/2016.1.112/kendo.all.min.js")
                 .Include("~/Scripts/kendo/2016.1.112/kendo.aspnetmvc.min.js")
                 .Include("~/Scripts/kendo/2016.1.112/.modernizr.custom.js")
                 );
        }
    }
}
