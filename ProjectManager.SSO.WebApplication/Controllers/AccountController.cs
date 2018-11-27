using ProjectManager.Common.LoginUserInfo;
using ProjectManager.SSO.Models;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace ProjectManager.SSO.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class AccountController : Controller
    {
        /// <summary>
        /// TODO 加密的参数传递
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        //[AllowAnonymous]
        //[HttpGet]
        private ActionResult Index(string parameter)
        {
            // TODO 对参数进行解密
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="redirectUrl"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Index(string appId, string ssoUrl, string redirectUrl)
        {
            Account model = new Account();
            model.AppID = appId;
            model.RedirectUrl = redirectUrl;
            model.SSOUrl = ssoUrl;

            // 检查当前用户是否已经登陆了
            if (SSO.Authorize.Helper.GetAuthCookie(this.HttpContext) != null)
            {
                // TODO 获取 ticket
                string ticket = "a";
                // 跳转到 网站接收处理单点登陆的url上
                return Redirect(ssoUrl + "?ticket=" + ticket + "&returnUrl=" + model.RedirectUrl);
            }

            return View(model);
        }


        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Account model)
        {
            // TODO 验证用户登陆信息
            // TODO 发行ticket
            string ticket = "a";

            // 用户登陆，登陆成功后登陆成功信息写入到cookie
            SSO.Authorize.Helper.SetAuthCookie(this.Response, Guid.Parse("00000000-0000-0000-0000-000000000001"), model.UserName, model.Password);

            // 跳转回 网站处理点单登陆的action，由该action写入本地cookie
            return Redirect(model.SSOUrl + "?ticket=" + ticket + "&returnUrl=" + model.RedirectUrl);
        }

    }
}