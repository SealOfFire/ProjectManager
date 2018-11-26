using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager.DAL.AuthorizeManager.AMModel
{
    [Serializable]
    [Table("user_role", Schema = "authorize_manager")]
    public class UserRole
    {
        [Column("user_ID", Order = 0)]
        [Key]
        [Required]
        public Guid UserID { get; set; }

        [Column("role_ID", Order = 1)]
        [Key]
        [Required]
        public Guid RoleID { get; set; }

        [ForeignKey("UserID")]
        public virtual UserInfo UserInfo { get; set; }

        [ForeignKey("RoleID")]
        public virtual Role Role { get; set; }
    }
}
