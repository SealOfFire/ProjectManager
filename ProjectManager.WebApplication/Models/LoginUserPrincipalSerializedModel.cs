using System;

namespace ProjectManager.WebApplication.Models
{
    [Serializable]
    public class LoginUserPrincipalSerializedModel
    {
        public string ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}