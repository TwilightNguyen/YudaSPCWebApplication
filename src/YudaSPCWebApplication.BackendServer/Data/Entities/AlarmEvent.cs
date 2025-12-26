using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbAlarmEvent")]
    public class AlarmEvent
    {
        [Key]
        [Column("intAEID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IntAEID { get; set; }

        [MaxLength(200)]
        [Column("strAEDesc", TypeName = "nvarchar(200)")]
        public string? StrAEDesc { get; set; }

        [Column("intAEActive")]
        public int? IntAEActive { get; set; }

        [MaxLength(50)]
        [Column("strRoleID", TypeName = "nvarchar(50)")]
        public string? StrRoleID { get; set; }

        [Column("intLineID")]
        public int? IntLineID { get; set; }

        [Column("intEnable")]
        public int? IntEnable { get; set; }

        [Column("intEventSent")]
        public int? IntEventSent { get; set; }
    }
}
