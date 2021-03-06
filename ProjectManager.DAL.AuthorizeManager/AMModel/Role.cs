﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager.DAL.AuthorizeManager.AMModel
{
    [Serializable]
    [Table("role", Schema = "authorize_manager")]
    public class Role
    {
        [Column("ID")]
        [Key]
        [Required]
        public Guid ID { get; set; }

        [Column("name")]
        [StringLength(50)]
        public string Name { get; set; }

        [Column("remark")]
        [StringLength(50)]
        public string Remark { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<WebAppFunctionRole> WebAppFunctionRoles { get; set; }
    }
}
