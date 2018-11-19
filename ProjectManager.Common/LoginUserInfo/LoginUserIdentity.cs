using System;
using System.Security.Principal;

namespace ProjectManager.Common.LoginUserInfo
{
    public class LoginUserIdentity : IIdentity
    {
        private Guid ID;
        // private string userName;
        // private string password;
        private bool isAuthenticated = false;

        public string Name { get { return this.ID.ToString("N"); } }

        public string AuthenticationType { get { return null; } }

        public bool IsAuthenticated { get { return isAuthenticated; } }

        public LoginUserIdentity(Guid id, bool isAuthenticated = false)
        {
            this.ID = id;
            this.isAuthenticated = isAuthenticated;
        }

    }
}
