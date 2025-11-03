using System.ComponentModel.DataAnnotations;

namespace YudaSPCWebApplication.BackendServer.Data.Entities
{
    public class OverviewDescription
    {
        [Key]
        public int IntAreaID { get; set; }

        public int IntProcessID { get; set; }

        public int IntCharacteristicID { get; set; }

        public int IntInspPlanID { get; set; }

        public int IntProductNameID { get; set; }

        public int IntInspPlanDataID { get; set; }

        public int IntProductionDataID { get; set; }

        public int IntProcessLineID { get; set; }

        [MaxLength(100)]
        public string? StrProcessLineCode { get; set; }

        [MaxLength(100)]
        public string? StrProcessLineName { get; set; }

        [MaxLength(100)]
        public string? StrNameArea { get; set; }


        [MaxLength(100)]
        public string? StrProcessName { get; set; }

        [MaxLength(200)]
        public string? StrCharacteristicName { get; set; }

        public int? IntMeasureTypeID { get; set; }

        [MaxLength(100)]
        public string? StrMeaType { get; set; }


        [MaxLength(100)]
        public string? StrInspPlanName { get; set; }


        [MaxLength(100)]
        public string? StrNameProduct { get; set; }

        [MaxLength(100)]
        public string? StrInternalModel { get; set; }

        [MaxLength(100)]
        public string? StrJobCode { get; set; }

        [MaxLength(100)]
        public string? StrPOCode { get; set; }

        public DateTime? DtStartTime { get; set; }

        public DateTime? DtEndTime { get; set; }

        public double? FtLCL { get; set; }

        public double? FtUCL { get; set; }

        public double? FtLCLS3 { get; set; }

        public double? FtUCLS3 { get; set; }

        public double? Mean { get; set; }
        public double? IntQuantity { get; set; }
        public double? IntLSpecWarming { get; set; }
        public double? IntHSpecWarming { get; set; }
    }
}
