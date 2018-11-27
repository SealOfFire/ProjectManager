using ProjectManager.Common.LoginUserInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManager.SSO.WebApplication.Controllers
{
    public class UserInfoController : Controller
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="ticket"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetUserInfo(string appId, string secret, string ticket)
        {
            // TODO 验证用户信息
            // 从cookie中获取ticket，查看是否是当前登陆的用户
            LoginUserPrincipalSerializedModel model = new LoginUserPrincipalSerializedModel();
            model.ID = Guid.Empty.ToString();
            model.UserName = "name";
            model.Password = "password";

            return Json(model, JsonRequestBehavior.AllowGet);
        }
    }
}