using System.ComponentModel.DataAnnotations;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    public class View_InspectionPlanData
    {
        [Key]
        public int IntAreaID { get; set; }

        public int? IntID { get; set; }

        [MaxLength(100)]
        public string? StrNameArea { get; set; }

        public int? IntInspPlanID { get; set; }

        [MaxLength(100)]
        public string? StrInspPlanName { get; set; }

        public int? IntMeaTypeID { get; set; }

        [MaxLength(100)]
        public string? StrMeaType { get; set; }

        public DateTime? DtCreateTime { get; set; }

        public DateTime? DtUpdateTime { get; set; }

        public int? IntPlanState { get; set; }

        public int? IntCharacteristicType { get; set; }

        public int? IntPlanTypeID { get; set; }

        [MaxLength(100)]
        public string? StrPlanTypeName { get; set; }

        public double? FtLCL { get; set; }

        public double? FtLCLS3 { get; set; }

        public double? FtUCL { get; set; }

        public double? FtUCLS3 { get; set; }

        public double? FtCpkMax { get; set; }

        public double? FtCpkMin { get; set; }

        public bool? BoolCpkControl { get; set; }

        public bool? BoolDataEntry { get; set; }

        public bool? BoolSPCChart { get; set; }

        public int? IntCharacteristicOrder { get; set; }

        public int? IntCharacteristicID { get; set; }

        [MaxLength(200)]
        public string? StrCharacteristicName { get; set; }

        [MaxLength(100)]
        public string? StrCharacteristicUnit { get; set; }

        public int? IntProcessID { get; set; }

        [MaxLength(100)]
        public string? StrProcessName { get; set; }

        public bool? BoolPlanDeleted { get; set; }

        public bool? BoolDeleted { get; set; }

        public int? IntDecimals { get; set; }

        public double? FtPercentControlLimit { get; set; }

        [MaxLength(20)]
        public string? StrSampleSize { get; set; }
    }
}
