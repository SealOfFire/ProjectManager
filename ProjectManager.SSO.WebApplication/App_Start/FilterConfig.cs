using ProjectManager.SSO.WebApplication.Filter;
using System.Web.Mvc;

namespace ProjectManager.SSO.WebApplication
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new LogActionFilter());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
