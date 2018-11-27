using ProjectManager.WebApplication.Authorize;
using System.Web.Mvc;

namespace ProjectManager.WebApplication.Controllers
{
    /// <summary>
    /// 测试单点登陆验证
    /// </summary>
    [PMSSOAuthorize]
    public class SSOTestController : Controller
    {
        // GET: SSOTest
        public ActionResult Index()
        {
            return View();
        }
    }
}