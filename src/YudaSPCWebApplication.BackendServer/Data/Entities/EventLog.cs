using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbEventLog")]
    public class EventLog
    {
        [Key]
        [Column("intEventID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IntEventID { get; set; }

        [Column("dtEventTime", TypeName = "datetime")]
        public DateTime? DtEventTime { get; set; }

        [MaxLength(10)]
        [Column("strEventCode", TypeName = "nvarchar(10)")]
        public string? StrEventCode { get; set; }

        [MaxLength(200)]
        [Column("strEvent", TypeName = "nvarchar(200)")]
        public string? StrEvent { get; set; }

        [MaxLength(30)]
        [Column("strStation", TypeName = "nvarchar(30)")]
        public string? StrStation { get; set; }
    }
}
