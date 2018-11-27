using ProjectManager.DAL;
using System.Data.Entity;

namespace ProjectManager.SSO.DAL
{
    public class SSODbContext : BaseDbContext
    {
        #region 数据库 model



        #endregion

        #region 构造函数

        public SSODbContext() : base("ssodb")
        {
            // this.Database.Log = delegate (string message) { DbLogger.Trace(message); };
        }

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<SSODbContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}
