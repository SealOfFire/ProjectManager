using ProjectManager.Log;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ProjectManager.SSO.WebApplication.Filter
{
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
            var message = String.Format("[Action Filter Log] | IP:[{4}] | controller:[{1}] action:[{2}] | [{0}] parameters:[{3}]", methodName, controllerName, actionName, parameters, GetIP());
            // Debug.WriteLine(message, "Action Filter Log");
            AutoLogger.Info(message);
        }

        #region ip

        private string GetIP()
        {
            return GetClientIP(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"],
                HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"],
                HttpContext.Current.Request.UserHostAddress);
        }

        public static string GetClientIP(string HTTP_X_FORWARDED_FOR, string REMOTE_ADDR, string UserHostAddress)
        {
            string
                userHostAddress = HTTP_X_FORWARDED_FOR;

            if (!string.IsNullOrEmpty(userHostAddress))
            {
                if (userHostAddress.IndexOf(".") < -1)
                    userHostAddress = "";
                else if ((userHostAddress.IndexOf(",") > -1) || (userHostAddress.IndexOf(";") > -1))
                {
                    userHostAddress = userHostAddress.Replace(" ", "").Replace("'", "").Replace("\"", "");
                    string[] strArray = userHostAddress.Split(",;".ToCharArray());
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        if (IsIP(strArray[i]) && !strArray[i].StartsWith("192.168") && !strArray[i].StartsWith("127.0") && !strArray[i].StartsWith("127.000"))
                            return strArray[i];
                    }
                }
                else
                {
                    if (IsIP(userHostAddress))
                        return userHostAddress;
                }
            }

            if (string.IsNullOrEmpty(userHostAddress))
                userHostAddress = REMOTE_ADDR;

            if (string.IsNullOrEmpty(userHostAddress))
                userHostAddress = UserHostAddress;

            return userHostAddress;
        }

        public static bool IsIPV4(string ip)
        {
            if ((string.IsNullOrEmpty(ip)) || (ip.Length < 7) || (ip.Length > 15))
                return false;

            return Regex.IsMatch(ip, @"^\d{1,3}[.]\d{1,3}[.]\d{1,3}[.]\d{1,3}$", RegexOptions.IgnoreCase);
        }

        public static bool IsIPV6(string ip)
        {
            if ((string.IsNullOrEmpty(ip)) || (ip.Length < 11) || (ip.Length > 23))
                return false;

            return Regex.IsMatch(ip, @"^\d{1,3}[.]\d{1,3}[.]\d{1,3}[.]\d{1,3}[.]\d{1,3}[.]\d{1,3}$", RegexOptions.IgnoreCase);
        }

        public static bool IsIP(string ip)
        {
            return IsIPV4(ip) || IsIPV6(ip);
        }

        #endregion
    }
}