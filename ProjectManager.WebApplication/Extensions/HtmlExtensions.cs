using System.Web.Mvc;

namespace ProjectManager.WebApplication.Extensions
{
    /// <summary>
    /// 扩展HTML
    /// </summary>
    public static class HtmlExtensions
    {
        /// <summary>
        /// 带权限的链接生成器
        /// </summary>
        public static MvcHtmlString ActionAuth(this HtmlHelper htmlHelper, string actionName, string controllerName)
        {
            // TODO 判断当前用户是否有查看 Action 的权限
            // HttpContext.Current.User; // 用户信息

            TagBuilder tagBuilder = new TagBuilder("a");
            // tagBuilder.InnerHtml = linkText;
            // tagBuilder.MergeAttribute("href", url);
            // tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            return MvcHtmlString.Create(tagBuilder.ToString());
        }
    }
}