using Newtonsoft.Json;
using ProjectManager.Common.LoginUserInfo;
using System;
using System.IO;
using System.Net;
using System.Web.Mvc;

namespace ProjectManager.WebApplication.Controllers
{
    /// <summary>
    /// 单点登陆成功
    /// </summary>
    public class SSOController : Controller
    {
        /// <summary>
        /// 单点登陆成功
        /// </summary>
        /// <param name="ticket">从单点登陆系统获取的票据</param>
        /// <param name="returnUrl">单点登陆成功后，跳转的本地地址</param>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult LoginSuccess(string ticket, string returnUrl)
        {
            // TODO 通过ticket从单点登陆中获取用户信息
            // TODO 也可以改成web service接口
            string url = string.Format("http://localhost:65114/UserInfo/GetUserInfo?appId={0}&secret={1}&ticket={2}",
                SSO.Authorize.Helper.GetSSOAppID(),
                SSO.Authorize.Helper.GetSSOSecret(),
                ticket);
            WebRequest request = WebRequest.Create(url);
            request.Method = "GET";
            HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            StreamReader streamReader = new StreamReader(response.GetResponseStream());
            string responseContent = streamReader.ReadToEnd();
            LoginUserPrincipalSerializedModel json = JsonConvert.DeserializeObject<LoginUserPrincipalSerializedModel>(responseContent);


            // TODO 票据写入到cookie中作为登陆信息
            SSO.Authorize.Helper.SetAuthCookie(this.Response, Guid.Parse(json.ID), json.UserName, json.Password);

            return Redirect(returnUrl);
        }
    }
}