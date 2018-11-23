using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager.DAL.ProjectManager.PMModel
{
    /// <summary>
    /// 部门
    /// </summary>
    [Serializable]
    [Table("department", Schema = "project_manager")]
    public class Department
    {
        #region 数据库映射属性

        /// <summary>
        /// 部门ID
        /// </summary>
        [Column("ID")]
        [Key]
        [Required]
        public Guid ID { get; set; }

        /// <summary>
        /// 部门名称
        /// </summary>
        [Column("name")]
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 部门所属的所有用户
        /// </summary>
        public virtual ICollection<UserInfo> Users { get; set; }

        #endregion
    }
}
