using ProjectManager.Common;
using ProjectManager.WebApplication.Models;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace ProjectManager.WebApplication.Controllers
{
    public class AccountController : Controller
    {
        // https://www.codeproject.com/tips/574576/how-to-implement-a-custom-iprincipal-in-asp-net-mv
        // https://www.c-sharpcorner.com/UploadFile/0adfed/authentication-and-authorization-using-iprincipal-interface/

        // GET: Account
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string userName, string password)
        {
            // TODO 验证用户登陆


            // 用户登陆ticket写入到cookie
            this.CreateAuthenticationTicket(Guid.Parse("00000000-0000-0000-0000-000000000001"), userName, password);
            return Content("登陆");
        }

        /// <summary>
        /// 用户登陆信息写道客户端cookies
        /// </summary>
        /// <param name="userName"></param>
        public void CreateAuthenticationTicket(Guid Id, string userName, string password)
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
            Response.Cookies.Add(faCookie);
        }
    }
}