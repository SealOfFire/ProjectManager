using ProjectManager.Log;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProjectManager.WebApplication.Filter
{
    // 记录日志
    public class LogActionFilter : ActionFilterAttribute
    {
        /// <summary>
        /// 开始执行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // HttpContext.Current.User
            // 获取用户登陆信息，写入到日志
            Log("OnActionExecuting", filterContext.RouteData, filterContext.ActionParameters);
        }

        /// <summary>
        /// 执行结束
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Log("OnActionExecuted", filterContext.RouteData);
        }

        /// <summary>
        /// 结果开始执行
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            Log("OnResultExecuting", filterContext.RouteData);
        }

        /// <summary>
        /// 结果执行结束
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            Log("OnResultExecuted", filterContext.RouteData);
        }

        private void Log(string methodName, RouteData routeData, IDictionary<string, object> actionParameters = null)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];

            System.Text.StringBuilder parameters = new System.Text.StringBuilder();
            if (actionParameters != null)
            {

                foreach (KeyValuePair<string, object> kvp in actionParameters)
                {
                    if (parameters.Length > 0) parameters.Append("&");
                    parameters.Append(kvp.Key + "=" + kvp.Value);
                }
                parameters.Insert(0, "?");
            }
            var message = String.Format("[Action Filter Log] | controller:[{1}] action:[{2}] | [{0}] parameters:[{3}]", methodName, controllerName, actionName, parameters);
            // Debug.WriteLine(message, "Action Filter Log");
            AutoLogger.Info(message);
        }

    }
}