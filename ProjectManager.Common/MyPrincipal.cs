using System;
using System.Collections;
using System.Security.Principal;

namespace ProjectManager.Common
{
    [Obsolete("测试用的代码")]
    public class MyPrincipal : IPrincipal
    {
        private IIdentity identity;
        private ArrayList roleList;
        private string userName;

        public IIdentity Identity { get { return this.identity; } set { this.identity = value; } }
        public ArrayList RoleList { get { return roleList; } }

        public MyPrincipal(string userID, string password)
        {
            // 
            // TODO: 在此处添加构造函数逻辑 
            // 
            identity = new MyIdentity(userID, password);
            if (identity.IsAuthenticated)
            {
                // 如果通过验证则获取该用户的Role，这里可以修改为从数据库中 
                // 读取指定用户的Role并将其添加到Role中，本例中直接为用户添加一个Admin角色 
                roleList = new ArrayList();
                roleList.Add("Admin");
            }
            else
            {
                // do nothing
            }
        }

        /// <summary>
        /// 判断用户是否有权限
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        bool IPrincipal.IsInRole(string role)
        {
            return roleList.Contains(role); ;
        }
    }
}
