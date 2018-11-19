using ProjectManager.DAL.AuthorizeManager.AMModel;
using ProjectManager.Log;
using System.Data.Entity;

namespace ProjectManager.DAL.AuthorizeManager
{
    /// <summary>
    /// 权限管理
    /// </summary>
    public class AMDbContext : BaseDbContext
    {
        #region 数据库 model

        public DbSet<Operate> Operates { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<WebAppFunction> WebAppFunctions { get; set; }
        public DbSet<WebAppFunctionRole> WebAppFunctionRoles { get; set; }

        #endregion

        #region 构造函数

        public AMDbContext() : base("amdb")
        {
            this.Database.Log = delegate (string message) { AMDbLogger.Trace(message); };
        }

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<AMDbContext>(null);
            base.OnModelCreating(modelBuilder);
        }
    }
}
