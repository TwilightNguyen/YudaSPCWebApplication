using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbEventEmailData")]
    public class EventEmailData
    {
        [Key]
        [Column("intID")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IntID { get; set; }

        [Column("intEventRoleID")]
        public int? IntEventRoleID { get; set; }

        [Column("intProductionID")]
        public int? IntProductionID { get; set; }

        [Column("intDefectRate")]
        public int? IntDefectRate { get; set; }

        [Column("intAEActive")]
        public int? IntAEActive { get; set; }

        [Column("dtTimeIn")]
        public int? IntEventSent { get; set; }

        [Column("intEventSentFail")]
        public int? IntEventSentFail { get; set; }

        [Column("dtUpdateTime", TypeName = "datetime")]
        public DateTime? DtUpdateTime { get; set; }

        [Column("intOutSpec")]
        public int? IntOutSpec { get; set; }

        [Column("intQty")]
        public int? IntQty { get; set; }

        [Column("dtEmailSentTime", TypeName = "datetime")]
        public DateTime? DtEmailSentTime { get; set; }

    }
}
