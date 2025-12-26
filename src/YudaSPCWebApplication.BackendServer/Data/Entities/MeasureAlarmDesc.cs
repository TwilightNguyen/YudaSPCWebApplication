using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbMeasureAlarmDesc")]
    public class MeasureAlarmDesc
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("intID")]
        public int IntID { get; set; }

        [Column("intAlarmValue")]
        public int? IntAlarmValue { get; set; }

        [MaxLength(50)]
        [Column("strAlarmDesc", TypeName = "nvarchar(50)")]
        public string? StrAlarmDesc { get; set; }
    }
}
