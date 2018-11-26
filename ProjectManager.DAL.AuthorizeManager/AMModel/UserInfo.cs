using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager.DAL.AuthorizeManager.AMModel
{
    [Serializable]
    [Table("user_info", Schema = "authorize_manager")]
    public class UserInfo
    {
        [Column("ID")]
        [Key]
        [Required]
        public Guid ID { get; set; }

        [Column("user_name")]
        [Required]
        [StringLength(50)]
        public string UserName { get; set; }

        [Column("password")]
        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
