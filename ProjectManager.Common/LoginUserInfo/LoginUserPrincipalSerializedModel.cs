using System;

namespace ProjectManager.Common.LoginUserInfo
{
    [Serializable]
    public class LoginUserPrincipalSerializedModel
    {
        public string ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
