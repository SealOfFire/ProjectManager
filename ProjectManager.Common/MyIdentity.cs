using System;
using System.Security.Principal;

namespace ProjectManager.Common
{
    [Obsolete("测试用的代码")]
    public class MyIdentity : IIdentity
    {
        public string Name
        {
            get
            {
                // TODO:   添加 MyIdentity.Name getter 实现 
                return userID;
            }
        }

        //这个属性我们可以根据自己的需要来灵活使用,在本例中没有用到它 
        public string AuthenticationType {
            get
            {
                // TODO:   添加 MyIdentity.AuthenticationType getter 实现 
                return null;
            }
        }

        public bool IsAuthenticated
        {
            get
            {
                // TODO: 添加 MyIdentity.IsAuthenticated getter 实现 
                return CanPass();
            }
        }

        private string userID;
        private string password;

        public MyIdentity(string currentUserID, string currentPassword)
        {
            // 
            // TODO: 在此处添加构造函数逻辑 
            // 
            userID = currentUserID;
            password = currentPassword;
        }

        private bool CanPass()
        {
            //这里朋友们可以根据自己的需要改为从数据库中验证用户名和密码， 
            //这里为了方便我直接指定的字符串 
            if (userID == "yan0lovesha" && password == "iloveshasha")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
