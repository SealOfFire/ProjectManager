using System;
using System.Web;
using System.Web.Mvc;

namespace ProjectManager.SSO.Authorize
{
    public class SSOAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            //// 验证单点登陆用户信息
            //if (httpContext == null)
            //{
            //    throw new ArgumentNullException("HttpContext");
            //}
            //// 判断用户权限
            //if (!httpContext.User.Identity.IsAuthenticated)
            //    return false;

            //// 验证用户信息


            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            // TODO 验证用户信息

            base.OnAuthorization(filterContext);

            // 验证失败，返回到单点登陆界面
            if (filterContext.Result is HttpUnauthorizedResult)
            {
                // TODO 对于发送到单点登陆的参数进行加密
                // 跳转到单点登陆的登陆页面
                // 参数，1.网站ID，2.登陆成功后的跳转页面
                // 参数从配置文件中获取 1.app id，2.本地接收单点登陆成功回传的页面
                string appId = Helper.GetSSOAppID();
                string ssoAction = Helper.GetOSSLoginSuccessUrl();
                string url = Helper.GetOSSServiceUrl() + string.Format("/Account?appId={0}&ssoUrl={1}&redirectUrl={2}",
                    appId,
                    ssoAction,
                    filterContext.HttpContext.Request.RawUrl);
                filterContext.Result = new RedirectResult(url);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}
