using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager.DAL.ProjectManager.PMModel
{
    [Serializable]
    [Table("user_info", Schema = "project_manager")]
    public class UserInfo
    {
        #region 数据库映射属性

        /// <summary>
        /// 用户ID
        /// </summary>
        [Column("ID")]
        [Key]
        [Required]
        public Guid ID { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        [Column("department_ID")]
        public Guid? DepartmentID { get; set; }

        /// <summary>
        /// 登陆用户名
        /// </summary>
        [Column("user_name")]
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        /// <summary>
        /// 登陆密码
        /// </summary>
        [Column("password")]
        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [ForeignKey("DepartmentID")]
        public virtual Department Department { get; set; }

        #endregion

    }
}
