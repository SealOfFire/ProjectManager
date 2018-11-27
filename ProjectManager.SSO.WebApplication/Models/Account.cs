namespace ProjectManager.SSO.Models
{
    public class Account
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AppID { get; set; }

        /// <summary>
        /// 登陆成功后 网站需要跳转的url
        /// </summary>
        public string RedirectUrl { get; set; }

        /// <summary>
        /// 接受登陆成功后的 网站 处理
        /// </summary>
        public string SSOUrl { get; set; }
    }
}