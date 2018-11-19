using ProjectManager.DAL.AuthorizeManager;
using ProjectManager.DAL.AuthorizeManager.AMModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManager.WebApplication.Authorize
{
    /// <summary>
    /// 权限管理
    /// </summary>
    public class PMAuthorizeAttribute : AuthorizeAttribute
    {
        public new List<Role> Roles { get; set; }

        // public new string[] Roles { get; set; }

        /// <summary>
        /// 是否通过验证
        /// </summary>
        /// <param name="httpContext"></param>
        /// <returns></returns>
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (httpContext == null)
            {
                throw new ArgumentNullException("HttpContext");
            }

            // TODO 从session中获取用户登陆信息

            // 判断用户权限
            if (!httpContext.User.Identity.IsAuthenticated)
                return false;

            // 没有权限管理的页面
            //if (Roles == null)
            //{
            //    return true;
            //}

            //if (Roles.Length == 0)
            //{
            //    return true;
            //}

            //if (Roles.Any(httpContext.User.IsInRole))
            //{
            //    return true;
            //}

            // 从session 中获取当前用户，判断当前用户的角色是否包含访问允许的角色
            if (this.Roles != null)
            {
                using (AMDbContext amctx = new AMDbContext())
                {
                    UserInfo ui = amctx.UserInfos.Find(Guid.Parse("00000000-0000-0000-0000-000000000001"));
                    if (ui != null)
                    {
                        foreach (UserRole role in ui.UserRoles)
                        {
                            if (this.Roles.Count(q => q.ID == role.Role.ID) > 0)
                            {
                                // 有访问权限
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            string actionName = filterContext.ActionDescriptor.ActionName;
            // string roles = GetRoles.GetActionRoles(actionName, controllerName);

            //if (!string.IsNullOrWhiteSpace(roles))
            //{
            //    this.Roles = roles.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries);
            //}

            using (AMDbContext amctx = new AMDbContext())
            {
                // 检索当前action可以访问的角色列表
                WebAppFunction fun = amctx.WebAppFunctions.Where(q => q.Controller == controllerName && q.Action == actionName).FirstOrDefault();
                if (fun != null)
                {
                    // 所有拥有可使用权限的角色列表
                    List<WebAppFunctionRole> funRoles = fun.WebAppFunctionRoles.Where(q => q.Operate.Name == "enable").ToList();
                    this.Roles = new List<Role>();
                    foreach (WebAppFunctionRole funRole in funRoles)
                    {
                        this.Roles.Add(funRole.Role);
                    }
                }
            }

            base.OnAuthorization(filterContext);

            // 验证失败时返回到登陆界面
            if (filterContext.Result is HttpUnauthorizedResult)
            {
                filterContext.Result = new RedirectToRouteResult(
                    new System.Web.Routing.RouteValueDictionary
                        {
                                { "langCode", filterContext.RouteData.Values[ "langCode" ] },
                                { "controller", "Account" },
                                { "action", "Index" },
                                { "ReturnUrl", filterContext.HttpContext.Request.RawUrl }
                        });
            }
        }

        /// <summary>
        /// 处理未授权的请求
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}