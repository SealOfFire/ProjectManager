using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManager.WebApplication.Controllers
{
    /// <summary>
    /// 测试单点登陆验证
    /// </summary>
    [SSO.Authorize.SSOAuthorize]
    public class SSOTestController : Controller
    {
        // GET: SSOTest
        public ActionResult Index()
        {
            return View();
        }
    }
}