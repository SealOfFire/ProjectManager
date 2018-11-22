using System.Web.Optimization;

namespace ProjectManager.WebApplication
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            // 脚本
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));

            // bootstrap 脚本
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js"));

            // bootstrap 样式
            bundles.Add(new StyleBundle("~/bundles/bootstrap").Include("~/Content/bootstrap.css"));

            // 自定义样式

        }
    }
}