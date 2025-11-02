using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    [Table("tbInspectionPlanData")]
    public class InspectionPlanData
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IntID { get; set; }

        public int? IntModelID { get; set; }

        public int? IntCharacteristicID { get; set; }

        public int? IntProcessID { get; set; }

        [MaxLength(200)]
        [Column(TypeName = "nvarchar(200)")]
        public string? StrCharacteristicName { get; set; }

        [MaxLength(100)]
        [Column(TypeName = "nvarchar(100)")]
        public string? StrProcessName { get; set; }

        public double? FtLCL { get; set; }

        public double? FtUCL { get; set; }

        public double? FtLCLS3 { get; set; }

        public double? FtUCLS3 { get; set; }

        public bool? BoolSPCChart { get; set; }

        public bool? BoolDataEntry { get; set; }

        public bool? BoolDeleted { get; set; }

        [MaxLength(20)]
        [Column(TypeName = "nvarchar(20)")]
        public string? StrSampleSize { get; set; }
    }
}
