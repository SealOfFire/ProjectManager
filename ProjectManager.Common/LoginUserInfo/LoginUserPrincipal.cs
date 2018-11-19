using System;
using System.Security.Principal;

namespace ProjectManager.Common.LoginUserInfo
{
    public class LoginUserPrincipal : IPrincipal
    {
        private IIdentity identity;
        private Guid id;
        private string userName;
        private string password;

        public IIdentity Identity { get { return this.identity; } }
        public Guid ID { get { return this.id; } }
        public string UserName { get { return this.userName; } }
        public string Password { get { return this.password; } }

        public LoginUserPrincipal(Guid id, string userName, string password)
        {
            // TODO 验证用户名和密码

            // TODO 获取用户角色列表

            this.id = id;
            this.userName = userName;
            this.password = password;
            this.identity = new LoginUserIdentity(id, true);
        }

        public bool IsInRole(string role)
        {
            throw new NotImplementedException();
        }
    }
}
