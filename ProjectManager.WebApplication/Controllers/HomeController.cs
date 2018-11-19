using ProjectManager.DAL.ProjectManager;
using ProjectManager.DAL.ProjectManager.PMModel;
using ProjectManager.Log;
using ProjectManager.WebApplication.Authorize;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ProjectManager.WebApplication.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [PMAuthorize]
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            Logger.Trace("start");

            using (PMDbContext context = new PMDbContext())
            {
                int debug = 0;
                debug++;
                List<UserInfo> a = context.UserInfo.ToList();
                a.FirstOrDefault().Password = "3";
                context.SaveChanges();
                debug++;
            }
            Logger.Trace("finish");

            return View();
        }

        // GET: Home
        public ActionResult Test1()
        {
            Logger.Trace("start");

            using (PMDbContext context = new PMDbContext())
            {
                int debug = 0;
                debug++;
                List<UserInfo> a = context.UserInfo.ToList();
                a.FirstOrDefault().Password = "1";
                context.SaveChanges();
                debug++;
            }
            Logger.Trace("finish");

            return View();
        }

        // GET: Home
        public ActionResult Test2()
        {
            Logger.Trace("start");

            using (PMDbContext context = new PMDbContext())
            {
                int debug = 0;
                debug++;
                List<UserInfo> a = context.UserInfo.ToList();
                a.FirstOrDefault().Password = "1";
                context.SaveChanges();
                debug++;
            }
            Logger.Trace("finish");

            return View();
        }
    }
}