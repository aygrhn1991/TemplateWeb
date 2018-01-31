using System.Web;
using System.Web.Optimization;

namespace TemplateWeb
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css-lib").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/font-awesome.css"));

            bundles.Add(new ScriptBundle("~/bundles/script-lib").Include(
                      "~/Scripts/jquery-2.2.4.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/angular.js"));

            bundles.Add(new StyleBundle("~/bundles/css-public").Include(
                      "~/Plugin/ngtable/css/ng-table.css"));

            bundles.Add(new ScriptBundle("~/bundles/script-public").Include(
                      "~/Plugin/ngtable/js/ng-table.js",
                      "~/Plugin/layer/layer.js"));
        }
    }
}