using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager.DAL.AuthorizeManager.AMModel
{
    [Serializable]
    [Table("webapp_function_role")]
    public class WebAppFunctionRole
    {
        [Column("role_ID", Order = 0)]
        [Key]
        [Required]
        public Guid RoleID { get; set; }

        [Column("function_ID", Order = 1)]
        [Key]
        [Required]
        public Guid FunctionID { get; set; }

        [Column("operate_ID", Order = 2)]
        [Key]
        [Required]
        public Guid OperateID { get; set; }

        [ForeignKey("RoleID")]
        public virtual Role Role { get; set; }

        [ForeignKey("FunctionID")]
        public virtual WebAppFunction WebAppFunction { get; set; }

        [ForeignKey("OperateID")]
        public virtual Operate Operate { get; set; }
    }
}
