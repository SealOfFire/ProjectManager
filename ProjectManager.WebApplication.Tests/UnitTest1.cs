using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProjectManager.DAL.AuthorizeManager;
using ProjectManager.DAL.ProjectManager;
using ProjectManager.DAL.ProjectManager.PMModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProjectManager.WebApplication.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

        }

        [TestMethod]
        public void TestMethod2()
        {
            using (PMDbContext context = new PMDbContext())
            {
                int debug = 0;
                debug++;
                List<UserInfo> a = context.UserInfo.ToList();
                a.FirstOrDefault().Password = "10";

                // context.UserInfo.as

                context.SaveChanges();
                debug++;
            }
        }

        [TestMethod]
        public void TestMethod3()
        {
            using (PMDbContext context = new PMDbContext())
            {
                UserInfo ui = new UserInfo();
                ui.ID = Guid.NewGuid();
                ui.UserName = "b";
                ui.Password = "a";
                context.UserInfo.Add(ui);
                // 
                Department department = new Department();
                department.ID = Guid.NewGuid();
                department.Name = "c";
                department.Users = new List<UserInfo>();
                department.Users.Add(ui);
                ui.Department = department;

                int count = context.SaveChanges();
                Log.Logger.Trace("TestMethod3:", count);
            }
        }

        [TestMethod]
        public void TestMethod4()
        {
            using (AMDbContext context = new AMDbContext())
            {
                int debug = 0;
                debug++;

                var a = context.UserInfos.ToList();

                debug++;
            }
        }
    }
}
