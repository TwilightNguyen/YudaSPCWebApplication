using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    public class MeasData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IntID { get; set; }

        public int? IntCharacteristicID { get; set; }

        [MaxLength(64)]
        [Column(TypeName = "nvarchar(64)")]
        public string? VarCharacteristicValue { get; set; }

        [MaxLength(64)]
        [Column(TypeName = "nvarchar(64)")]
        public string? VarCharacteristicRange { get; set; }

        public DateTime? DtTimeStamp { get; set; }

        public int? IntDataCollection { get; set; }

        public DateTime? DtTimeMeasure { get; set; }

        public int? IntLineID { get; set; }
        public int? IntJobID { get; set; }
        public int? IntProductionID { get; set; }
        public int? IntOutputQty { get; set; }
        public int? IntSampleIndex { get; set; }
        public int? IntUserID { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string? StrFullName { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string? StrOutputNotes { get; set; }

        public int? IntSampleQty { get; set; }
        public int? IntMoldID { get; set; }
        public int? IntCavityID { get; set; }

        [MaxLength(50)]
        [Column(TypeName = "nvarchar(50)")]
        public string? StrPlanTypeName { get; set; }
    }
}
