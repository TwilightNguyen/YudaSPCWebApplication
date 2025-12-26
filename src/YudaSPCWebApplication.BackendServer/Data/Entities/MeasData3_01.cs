using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbMeasData3_01")]
    public class MeasData3_01
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("intID")]
        public int IntID { get; set; }

        [Column("intCharacteristicID")]
        public int? IntCharacteristicID { get; set; }

        [MaxLength(64)]
        [Column("varCharacteristicValue", TypeName = "nvarchar(64)")]
        public string? VarCharacteristicValue { get; set; }

        [MaxLength(64)]
        [Column("varCharacteristicRange", TypeName = "nvarchar(64)")]
        public string? VarCharacteristicRange { get; set; }

        [Column("dtTimeStamp", TypeName = "datetime")]
        public DateTime? DtTimeStamp { get; set; }

        [Column("intDataCollection")]
        public int? IntDataCollection { get; set; }

        [Column("dtTimeMeasure", TypeName = "datetime")]
        public DateTime? DtTimeMeasure { get; set; }

        [Column("intJobID")]
        public int? IntJobID { get; set; }

        [Column("intProductionID")]
        public int? IntProductionID { get; set; }

        [Column("intLineID")]
        public int? IntLineID { get; set; }

        [Column("intUserID")]
        public int? IntUserID { get; set; }

        [Column("intOutputQty")]
        public int? IntOutputQty { get; set; }

        [Column("intSampleIndex")]
        public int? IntSampleIndex { get; set; }

        [Column("intOKNG")]
        public int? IntOKNG { get; set; }

        [Column("intEmailSent")]
        public int? IntEmailSent { get; set; }

        [Column("intPlanTypeID")]
        public int? IntPlanTypeID { get; set; }

        [MaxLength(50)]
        [Column("strOutputNotes", TypeName = "nvarchar(50)")]
        public string? StrOutputNotes { get; set; }

        [Column("intSampleQty")]
        public int? IntSampleQty { get; set; }

        [Column("intMoldID")]
        public int? IntMoldID { get; set; }

        [Column("intCavityID")]
        public int? IntCavityID { get; set; } 
    }
}
