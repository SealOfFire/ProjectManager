using ProjectManager.WebApplication.Filter;
using System.Web.Mvc;

namespace ProjectManager.WebApplication
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // 增加过滤器会自动运行，不管是否声明Controller属性
            // filters.Add(new PMAuthorizeAttribute());
            filters.Add(new LogActionFilter());
        }
    }
}