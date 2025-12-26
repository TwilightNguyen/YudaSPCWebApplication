using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbProductionData")]
    public class ProductionData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("intID")]
        public int IntID { get; set; }

        [Column("intLineID")]
        public int? IntLineID { get; set; }

        [Column("dtStartTime", TypeName = "datetime")]
        public DateTime? DtStartTime { get; set; }

        [Column("dtEndTime", TypeName = "datetime")]
        public DateTime? DtEndTime { get; set; }

        [Column("intJobID")]
        public int? IntJobID { get; set; }

        [Column("boolDeleted")]
        public bool? BoolDeleted { get; set; }

        [Column("intUserID")]
        public int? IntUserID { get; set; }

        [MaxLength(100)]
        [Column("strNotes", TypeName = "nvarchar(100)")]
        public string? StrNotes { get; set; }

        [Column("dtProductionDate", TypeName = "datetime")]
        public DateTime? DtProductionDate { get; set; }

        [Column("intProductionQty")]
        public int? IntProductionQty { get; set; }

        [MaxLength(100)]
        [Column("strLotInform", TypeName = "nvarchar(100)")]
        public string? StrLotInform { get; set; }

        [MaxLength(50)]
        [Column("strMaterialInform", TypeName = "nvarchar(50)")]
        public string? StrMaterialInform { get; set; }

        [Column("intCNCLatheMachine")]
        public int? IntCNCLatheMachine { get; set; }
    }
}
