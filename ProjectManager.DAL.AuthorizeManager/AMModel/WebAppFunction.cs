using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager.DAL.AuthorizeManager.AMModel
{
    [Serializable]
    [Table("webapp_function", Schema = "authorize_manager")]
    public class WebAppFunction
    {
        [Column("ID")]
        [Key]
        [Required]
        public Guid ID { get; set; }

        [Column("controller")]
        [Required]
        [StringLength(50)]
        public string Controller { get; set; }

        [Column("action")]
        [Required]
        [StringLength(50)]
        public string Action { get; set; }

        public virtual ICollection<WebAppFunctionRole> WebAppFunctionRoles { get; set; }
    }
}
