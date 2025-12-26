using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbEventEmailHistory")]
    public class EventEmailHistory 
    {
        [Key]
        [Column("intID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IntID { get; set; }

        [Column("intCharacteristicID")]
        public int? IntCharacteristicID { get; set; }

        [Column("intProductionID")]
        public int? IntProductionID { get; set; }

        [Column("intDefectRate")]
        public int? IntDefectRate { get; set; }

        [Column("intAEActive")]
        public int? IntAEActive { get; set; }

        [Column("dtTimeIn", TypeName = "datetime")]
        public DateTime? DtTimeIn { get; set; }

        [Column("strEmailAddress", TypeName = "nvarchar(max)")]
        public string? StrEmailAddress { get; set; }

    }
}
