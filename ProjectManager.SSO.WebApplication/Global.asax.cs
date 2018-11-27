using ProjectManager.Common.LoginUserInfo;
using ProjectManager.Log;
using System;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace ProjectManager.SSO.WebApplication
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutoLogger.Info("[Application_Start]:[begin]");

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            AutoLogger.Info("[Application_Start]:[end]");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            // 从cookie中获取登陆用户信息
            SSO.Authorize.Helper.GetAuthCookie(HttpContext.Current);
        }
    }
}
