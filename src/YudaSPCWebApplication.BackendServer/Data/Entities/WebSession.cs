using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbWebSession")]
    public class WebSession
    {
        [Key]
        [Column("intID")]
        public int IntID { get; set; }

        [MaxLength(100)]
        [Column("strSessionID", TypeName = "nvarchar(100)")]
        public string? StrSessionID { get; set; }

        [MaxLength(50)]
        [Column("strIpAddress", TypeName = "nvarchar(50)")]
        public string? StrIpAddress { get; set; }

        [Column("dtStartTime", TypeName = "datetime")]
        public DateTime? DtStartTime { get; set; }

        [Column("dtEndTime", TypeName = "datetime")]
        public DateTime? DtEndTime { get; set; }
    }
}
