using ProjectManager.Common.LoginUserInfo;
using System;
using System.Configuration;
using System.Security.Principal;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace ProjectManager.SSO.Authorize
{
    /// <summary>
    /// 
    /// </summary>
    public class Helper
    {
        public static string GetSSOAppID()
        {
            string value = ConfigurationManager.AppSettings["SSO_AppID"].ToString();
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("缺少《SSO_AppID》配置");
            }
            return value;
        }

        public static string GetSSOSecret()
        {
            string value = ConfigurationManager.AppSettings["SSO_Secret"].ToString();
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("缺少《SSO_Secret》配置");
            }
            return value;
        }

        /// <summary>
        /// 从配置文件中获取单点登陆服务器的地址
        /// </summary>
        /// <returns></returns>
        public static string GetOSSServiceUrl()
        {
            string value = ConfigurationManager.AppSettings["SSO_Url"].ToString();
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("缺少《单点登陆服务器地址（SSO_Url）》配置");
            }
            return value;
        }

        /// <summary>
        /// 从配置文件中获取单点登陆成功后 回调的本地网站地址
        /// </summary>
        /// <returns></returns>
        public static string GetOSSLoginSuccessUrl()
        {
            string value = ConfigurationManager.AppSettings["SSO_LoginSuccessUrl"].ToString();
            if (string.IsNullOrEmpty(value))
            {
                throw new Exception("缺少《单点登陆成功后本地的处理地址（SSO_LoginSuccessUrl）》配置");
            }
            return value;
        }

        /// <summary>
        /// 登陆信息写入到cookie中
        /// </summary>
        public static HttpCookie SetAuthCookie(HttpResponseBase response, Guid Id, string userName, string password)
        {
            LoginUserPrincipalSerializedModel serializeModel = new LoginUserPrincipalSerializedModel();
            serializeModel.ID = Id.ToString("N");
            serializeModel.UserName = userName;
            serializeModel.Password = password;
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            string userData = serializer.Serialize(serializeModel);

            FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
              1,
              userName,
              DateTime.Now,
              DateTime.Now.AddHours(8),
              false,
              userData);
            string encTicket = FormsAuthentication.Encrypt(authTicket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
            response.Cookies.Add(faCookie);
            return faCookie;
        }

        /// <summary>
        /// 从cookie中读取登陆信息
        /// </summary>
        public static IPrincipal GetAuthCookie(HttpContext httpContext)
        {
            // 从cookie中获取登陆用户信息
            HttpCookie authCookie = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (authTicket.UserData == "OAuth") return null;
                // 获取用户信息
                LoginUserPrincipalSerializedModel serializeModel = serializer.Deserialize<LoginUserPrincipalSerializedModel>(authTicket.UserData);
                // TODO 验证用户信息
                LoginUserPrincipal newUser = new LoginUserPrincipal(Guid.Parse(serializeModel.ID), serializeModel.UserName, serializeModel.Password);
                httpContext.User = newUser;
                return newUser;
            }
            return null;
        }

        /// <summary>
        /// 从cookie中读取登陆信息
        /// </summary>
        public static IPrincipal GetAuthCookie(HttpContextBase httpContext)
        {
            // 从cookie中获取登陆用户信息
            HttpCookie authCookie = httpContext.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                JavaScriptSerializer serializer = new JavaScriptSerializer();
                if (authTicket.UserData == "OAuth") return null;
                // 获取用户信息
                LoginUserPrincipalSerializedModel serializeModel = serializer.Deserialize<LoginUserPrincipalSerializedModel>(authTicket.UserData);
                // TODO 验证用户信息
                LoginUserPrincipal newUser = new LoginUserPrincipal(Guid.Parse(serializeModel.ID), serializeModel.UserName, serializeModel.Password);
                httpContext.User = newUser;
                return newUser;
            }
            return null;
        }
    }
}
