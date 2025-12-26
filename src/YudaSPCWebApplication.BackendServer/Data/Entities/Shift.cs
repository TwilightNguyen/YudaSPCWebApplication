using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbShift")]
    public class Shift
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("intID")]
        public int IntID { get; set; }

        [MaxLength(100)]
        [Column("StrNameShift", TypeName = "nvarchar(100)")]
        public string? StrNameShift { get; set; }

        [Column("dtStartTime", TypeName = "time")]
        public TimeSpan? DtStartTime { get; set; }

        [Column("dtEndTime", TypeName = "time")]
        public TimeSpan? DtEndTime { get; set; }
    }
}
