using MySql.Data.Entity;
using ProjectManager.Common.LoginUserInfo;
using ProjectManager.Log;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;
using System.Web;

namespace ProjectManager.DAL
{
    /// <summary>
    /// 
    /// </summary>
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class BaseDbContext : DbContext
    {
        #region 构造函数

        public BaseDbContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            DbInterception.Add(new CommandInterceptor());
            // this.Database.Log = delegate (string message) { DbLogger.Trace(message); };
        }

        #endregion

        #region 方法

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int SaveChanges()
        {
            this.CheckEntity();
            this.RecordDbChangeLog();
            return base.SaveChanges();
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="modelBuilder"></param>
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    // 去除code first的检测
        //    // modelBuilder.HasDefaultSchema("project_manager");
        //    // __migrationhistory
        //    // modelBuilder.Conventions.Remove<IncludeMetadataConvention>();
        //    Database.SetInitializer<BaseDbContext>(null);

        //    base.OnModelCreating(modelBuilder);
        //    // modelBuilder.Entity<AppInfo>().MapToStoredProcedures();
        //    // modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        //    // modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        //}

        /// <summary>
        /// 检查数据
        /// </summary>
        private void CheckEntity()
        {
            AutoLogger.Trace(string.Format("-- CheckEntity BEGIN --"));
            IEnumerable<DbEntityEntry> values = this.ChangeTracker.Entries();
            foreach (DbEntityEntry value in values)
            {
                // TODO 日志
                // AutoLogger.Info(string.Format("[{0}]:[CheckEntity BEGIN]", entityType.Name));
                if (value.Entity is BaseModel)
                {
                    BaseModel entity = value.Entity as BaseModel;
                    if (HttpContext.Current.User.Identity.IsAuthenticated)
                    {
                        LoginUserPrincipal user = HttpContext.Current.User as LoginUserPrincipal;
                        // 自动修改插入数据的插入用户和插入日期
                        if (value.State == EntityState.Added)
                        {
                            entity.InsertDate = DateTime.Now;
                            entity.InsertUser = user.ID;
                            entity.UpdateDate = DateTime.Now;
                            entity.UpdateUser = user.ID;
                        }
                        // 自动修改更新数据的更新用户和更新日期
                        if (value.State == EntityState.Modified)
                        {
                            entity.UpdateDate = DateTime.Now;
                            entity.UpdateUser = user.ID;
                        }
                        if (value.State == EntityState.Deleted || value.State == EntityState.Modified)
                        {
                            DateTime lastUpdateDate = (DateTime)value.GetDatabaseValues()["UpdateDate"];
                            if (entity.UpdateDate != lastUpdateDate)
                            {
                                throw new Exception("更新数据的排它日期错误");
                            }
                        }
                    }
                }
                // TODO 日志
                // AutoLogger.Info(string.Format("[{0}]:[CheckEntity END]", entityType.Name));
            }
            AutoLogger.Trace(string.Format("-- CheckEntity END --"));
        }

        /// <summary>
        /// 记录数据库日志
        /// </summary>
        private void RecordDbChangeLog()
        {
            AutoLogger.Trace(string.Format("-- RecordDbChangeLog BEGIN --"));
            // 比较改变的项目，保存修改日志
            // 所有改变的实体
            IEnumerable<DbEntityEntry> values = this.ChangeTracker.Entries();
            foreach (DbEntityEntry value in values)
            {
                // 没有改变的实体不做记录
                if (value.State == EntityState.Detached || value.State == EntityState.Unchanged)
                    continue;

                // 获取表名和字段名
                Type entityType = value.Entity.GetType();
                TableAttribute tableAttribute = (TableAttribute)Attribute.GetCustomAttribute(entityType, typeof(TableAttribute));

                AutoLogger.Info(string.Format("[{0}]:[修改记录 BEGIN]", entityType.Name));

                DbPropertyValues dbPropertyValues = value.CurrentValues;
                if (value.State == EntityState.Added)
                {
                    // 新加的数据，数据库中没有元数据
                }
                else
                {
                    // 修改或删除数据，数据库中有元数据
                    dbPropertyValues = value.GetDatabaseValues();
                }

                foreach (string propertyName in dbPropertyValues.PropertyNames)
                {
                    // 获取表名和字段名
                    PropertyInfo propertyInfo = entityType.GetProperty(propertyName);
                    ColumnAttribute columnAttribute = (ColumnAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(ColumnAttribute));

                    // 获取当前数据的主键

                    // 获取变更状态
                    ChangedEntity changedEntity = new ChangedEntity();
                    changedEntity.State = value.State;
                    changedEntity.TableName = tableAttribute.Name;
                    changedEntity.ColumnName = columnAttribute.Name;
                    changedEntity.CurrentValue = value.CurrentValues[propertyName];
                    if (value.State != EntityState.Added)
                    {
                        // 添加数据时，元数据为空
                        // 非添加数据时，才有元数据
                        changedEntity.DatabaseValue = dbPropertyValues[propertyName];
                        changedEntity.OriginalValue = value.OriginalValues[propertyName];
                    }
                    AutoLogger.Trace(changedEntity.ToString());
                }

                AutoLogger.Info(string.Format("[{0}]:[修改记录 END]", entityType.Name));
            }

            AutoLogger.Trace(string.Format("-- RecordDbChangeLog END --"));
        }

        #endregion
    }
}
