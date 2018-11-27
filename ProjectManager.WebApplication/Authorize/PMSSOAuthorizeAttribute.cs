using ProjectManager.DAL.AuthorizeManager;
using ProjectManager.DAL.AuthorizeManager.AMModel;
using ProjectManager.SSO.Authorize;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectManager.WebApplication.Authorize
{
    public class PMSSOAuthorizeAttribute : SSOAuthorizeAttribute
    {
        public new List<Role> Roles { get; set; }

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
            // TODO 验证用户信息
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
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
        }
    }
}