using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectManager.DAL
{
    [Serializable]
    public class BaseModel
    {
        [Column("insert_date")]
        public DateTime InsertDate { get; set; }

        [Column("insert_user")]
        public Guid InsertUser { get; set; }

        [Column("update_date")]
        public DateTime UpdateDate { get; set; }

        [Column("update_user")]
        public Guid UpdateUser { get; set; }
    }
}
