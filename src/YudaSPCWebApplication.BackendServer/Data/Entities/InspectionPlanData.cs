using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbInspectionPlanData")]
    public class InspectionPlanData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("intID")]
        public int IntID { get; set; }

        [Column("intInspPlanSubID")]
        public int? IntInspPlanSubID { get; set; }

        [Column("intCharacteristicID")]
        public int? IntCharacteristicID { get; set; }

        [Column("ftLSL")]
        public double? FtLSL { get; set; }

        [Column("ftUSL")]
        public double? FtUSL { get; set; }

        [Column("ftLCL")]
        public double? FtLCL { get; set; }

        [Column("ftUCL")]
        public double? FtUCL { get; set; }

        [Column("boolSPCChart")]
        public bool? BoolSPCChart { get; set; }

        [Column("boolDataEntry")]
        public bool? BoolDataEntry { get; set; }

        [Column("intPlanState")]
        public int? IntPlanState { get; set; }

        [Column("ftCpkMax")]
        public double? FtCpkMax { get; set; }

        [Column("ftCpkMin")]
        public double? FtCpkMin { get; set; }

        [Column("boolSpkControl")]
        public bool? BoolSpkControl { get; set; }

        [MaxLength(20)]
        [Column("strSampleSize", TypeName = "nvarchar(20)")]
        public string? StrSampleSize { get; set; }

        [Column("ftPercentControlLimit")]
        public double? FtPercentControlLimit { get; set; }

        [Column("boolDeleted")]
        public bool? BoolDeleted { get; set; }
    }
}
