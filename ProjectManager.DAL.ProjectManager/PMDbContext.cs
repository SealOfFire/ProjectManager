using ProjectManager.DAL.ProjectManager.PMModel;
using ProjectManager.Log;
using System.Data.Entity;

namespace ProjectManager.DAL.ProjectManager
{
    /// <summary>
    /// 项目管理
    /// </summary>
    public class PMDbContext : BaseDbContext
    {
        #region 数据库 model

        public DbSet<UserInfo> UserInfo { get; set; }
        public DbSet<Department> Departments { get; set; }

        #endregion

        #region 构造函数

        public PMDbContext() : base("mpdb")
        {
            this.Database.Log = delegate (string message) { PMDbLogger.Trace(message); };
        }

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<PMDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}