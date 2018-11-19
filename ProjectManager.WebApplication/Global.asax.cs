using MySql.Data.Entity;
using ProjectManager.Common.LoginUserInfo;
using ProjectManager.DAL.AuthorizeManager;
using ProjectManager.DAL.AuthorizeManager.AMModel;
using ProjectManager.Log;
using ProjectManager.WebApplication.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace ProjectManager.WebApplication
{
    public class Global : HttpApplication
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_Start(object sender, EventArgs e)
        {
            AutoLogger.Info("[Application_Start]:[begin]");

            // 在应用程序启动时运行的代码
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            // 数据库配置
            DbConfiguration.SetConfiguration(new MySqlEFConfiguration());

            AutoLogger.Info("[Application_Start]:[end]");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_Error(Object sender, EventArgs e)
        {
            Exception lastError = Server.GetLastError();
            AutoLogger.Error(lastError);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            // 从cookie中获取登陆用户信息
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (authTicket.UserData == "OAuth") return;
                // 获取用户信息
                LoginUserPrincipalSerializedModel serializeModel = serializer.Deserialize<LoginUserPrincipalSerializedModel>(authTicket.UserData);
                // TODO 验证用户信息
                LoginUserPrincipal newUser = new LoginUserPrincipal(Guid.Parse(serializeModel.ID), serializeModel.UserName, serializeModel.Password);
                HttpContext.Current.User = newUser;
            }
        }
    }
}